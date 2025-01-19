import React, { useEffect, useState } from "react";
import axios from "axios";
import {
  Button,
  TextField,
  FormControlLabel,
  Checkbox,
  Radio,
  RadioGroup,
  FormControl,
  FormLabel,
  Container,
  Typography,
  Box,
} from "@mui/material";
import { useNavigate } from "react-router-dom";

function QuizPage() {
  const [questions, setQuestions] = useState([]);
  const [email, setEmail] = useState("");
  const [answers, setAnswers] = useState({});
  const [score, setScore] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    axios
      .get("http://localhost:5234/api/quiz")
      .then((response) => {
        console.log("Fetched Questions:", response.data);
        if (response.data.$values && Array.isArray(response.data.$values)) {
          setQuestions(response.data.$values);
        } else {
          console.error(
            "Expected $values to be an array, but got:",
            response.data
          );
        }
      })
      .catch((error) => {
        console.error("Error fetching quiz questions:", error);
      });
  }, []);

  const handleAnswerChange = (questionId, value, isChecked = false) => {
    setAnswers((prev) => {
      const updatedAnswers = { ...prev };
      if (!updatedAnswers[questionId]) updatedAnswers[questionId] = [];

      if (typeof value === "string") {
        updatedAnswers[questionId] = [value];
      } else {
        if (isChecked) {
          updatedAnswers[questionId].push(value);
        } else {
          updatedAnswers[questionId] = updatedAnswers[questionId].filter(
            (v) => v !== value
          );
        }
      }

      return updatedAnswers;
    });
  };

  const handleSubmit = () => {
    const submitData = {
      email: email,
      answers: Object.entries(answers).map(([questionId, selectedOptions]) => ({
        quizId: parseInt(questionId),
        selectedOptions,
        textAnswer: selectedOptions[0] || null,
      })),
    };

    console.log("Submit Data:", submitData);

    axios
      .post("http://localhost:5234/api/quiz/submit", submitData)
      .then((response) => {
        setScore(response.data.score);
        alert("Quiz submitted successfully!");
      })
      .catch((error) => {
        console.error("Error submitting quiz:", error);
        alert("Failed to submit quiz.");
      });
  };

  return (
    <Container>
      <Typography variant="h4" gutterBottom>
        Quiz Page
      </Typography>

      <Box mb={2}>
        <TextField
          label="Enter your email"
          type="email"
          fullWidth
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          required
        />
      </Box>

      {questions.map((question) => (
        <Box key={question.id} mb={3}>
          <FormControl component="fieldset" fullWidth>
            <FormLabel component="legend">{question.question}</FormLabel>
            {question.type === 0 && (
              <RadioGroup
                value={answers[question.id] || ""}
                onChange={(e) =>
                  handleAnswerChange(question.id, e.target.value)
                }
              >
                {question.options.$values?.map((option) => (
                  <FormControlLabel
                    key={option.id}
                    value={option.id}
                    control={<Radio />}
                    label={option.text}
                  />
                ))}
              </RadioGroup>
            )}
            {question.type === 1 && (
              <div>
                {question.options.$values?.map((option) => (
                  <FormControlLabel
                    key={option.id}
                    control={
                      <Checkbox
                        checked={
                          answers[question.id]?.includes(option.id) || false
                        }
                        onChange={(e) =>
                          handleAnswerChange(
                            question.id,
                            option.id,
                            e.target.checked
                          )
                        }
                      />
                    }
                    label={option.text}
                  />
                ))}
              </div>
            )}
            {question.type === 2 && (
              <TextField
                label="Your answer"
                fullWidth
                value={answers[question.id] || ""}
                onChange={(e) =>
                  handleAnswerChange(question.id, e.target.value)
                }
              />
            )}
          </FormControl>
        </Box>
      ))}

      <Box mb={3}>
        <Button variant="contained" color="primary" onClick={handleSubmit}>
          Submit Quiz
        </Button>
      </Box>

      {score !== null && (
        <Box mb={3}>
          <Typography variant="h6">Your Score: {score}</Typography>
        </Box>
      )}

      <Box mb={3}>
        <Button
          variant="contained"
          color="secondary"
          onClick={() => navigate("/highscores")}
        >
          View High Scores
        </Button>
      </Box>
    </Container>
  );
}

export default QuizPage;
