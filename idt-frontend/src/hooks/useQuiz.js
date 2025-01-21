import { useState, useEffect } from "react";
import axios from "axios";

// Custom hook to fetch quiz questions and handle form data
const useQuiz = () => {
  const [questions, setQuestions] = useState([]);
  const [email, setEmail] = useState("");
  const [answers, setAnswers] = useState({});
  const [score, setScore] = useState(null);

  useEffect(() => {
    axios
      .get("http://localhost:5234/api/quiz")
      .then((response) => {
        console.log("Fetched Questions:", response.data);
        setQuestions(response.data);
      })
      .catch((error) => {
        console.error("Error fetching quiz questions:", error);
      });
  }, []);

  // Handle changes to form data (e.g., radio buttons, checkboxes, text inputs)
  const handleAnswerChange = (questionId, value, isChecked = false) => {
    setAnswers((prev) => {
      const updatedAnswers = { ...prev };

      if (!updatedAnswers[questionId]) updatedAnswers[questionId] = {};

      const question = questions.find((q) => q.id === questionId);

      if (question?.type === 0) {
        // For radio questions, only one option should be selected
        updatedAnswers[questionId] = {
          selectedOptions: [{ id: parseInt(value), quizId: questionId, text: "", isCorrect: false }],
          textAnswer: null,
        };
      } else if (question?.type === 1) {
        // Handle checkbox selection (multiple options)
        if (!updatedAnswers[questionId].selectedOptions) {
          updatedAnswers[questionId].selectedOptions = [];
        }

        const option = question.options.find((option) => option.id === value);

        if (isChecked) {
          updatedAnswers[questionId].selectedOptions.push({ id: value, quizId: questionId, text: option.text, isCorrect: option.isCorrect });
        } else {
          updatedAnswers[questionId].selectedOptions = updatedAnswers[questionId].selectedOptions.filter(
            (option) => option.id !== value
          );
        }
      } else if (question?.type === 2) {
        // For textbox questions
        updatedAnswers[questionId] = {
          selectedOptions: [],
          textAnswer: value,
        };
      }

      return updatedAnswers;
    });
  };

  const handleSubmit = () => {
    if (!email || Object.keys(answers).length === 0) {
      alert("Please provide your email and answer all questions.");
      return; // Prevent submission if email or answers are missing
    }

    const submitData = {
      email: email,
      quizId: 1, // The specific quizId you're submitting for
      selectedOptions: answers[1]?.selectedOptions.map((option) => ({
        quizId: 1,
        id: option.id,
        text: option.text,
        isCorrect: option.isCorrect,
      })) || [],
      textAnswer: answers[1]?.textAnswer || null,
    };

    console.log("Submit Data:", submitData); // Debugging log to see the final data sent

    axios
      .post("http://localhost:5234/api/quiz", submitData)
      .then((response) => {
        setScore(response.data.score);
        alert("Quiz submitted successfully!");
      })
      .catch((error) => {
        console.error("Error submitting quiz:", error.response ? error.response.data : error.message);
        alert("Failed to submit quiz. Please check the inputs and try again.");
      });
  };

  return {
    questions,
    email,
    setEmail,
    answers,
    setAnswers,
    score,
    handleAnswerChange,
    handleSubmit,
  };
};

export default useQuiz;
