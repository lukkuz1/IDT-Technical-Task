// src/components/QuizPage.js
import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Button, TextField, FormControlLabel, Checkbox, Radio, RadioGroup, FormControl, FormLabel, Container, Grid } from '@mui/material';

function QuizPage() {
  const [questions, setQuestions] = useState([]);
  const [email, setEmail] = useState('');
  const [answers, setAnswers] = useState([]);
  const [score, setScore] = useState(0);

  useEffect(() => {
    // Fetch quiz questions from the backend
    axios.get('http://localhost:5000/api/quiz')
      .then(response => {
        setQuestions(response.data);
      })
      .catch(error => {
        console.error("There was an error fetching the quiz questions!", error);
      });
  }, []);

  const handleAnswerChange = (questionId, optionId, isChecked) => {
    setAnswers(prevAnswers => {
      const updatedAnswers = [...prevAnswers];
      const answerIndex = updatedAnswers.findIndex(ans => ans.questionId === questionId);
      if (answerIndex > -1) {
        updatedAnswers[answerIndex].selectedOptions = isChecked
          ? [...updatedAnswers[answerIndex].selectedOptions, optionId]
          : updatedAnswers[answerIndex].selectedOptions.filter(id => id !== optionId);
      } else {
        updatedAnswers.push({ questionId, selectedOptions: isChecked ? [optionId] : [] });
      }
      return updatedAnswers;
    });
  };

  const handleSubmit = () => {
    // Prepare the answer data to send to the backend
    const submitData = {
      email: email,
      answers: answers.map(answer => ({
        quizId: answer.questionId,
        selectedOptions: answer.selectedOptions,
      })),
    };

    axios.post('http://localhost:5000/api/quiz/submit', submitData)
      .then(response => {
        setScore(response.data.score); // Assume the response contains the score
        alert('Quiz submitted successfully!');
      })
      .catch(error => {
        console.error("There was an error submitting the quiz!", error);
      });
  };

  return (
    <Container>
      <h2>Quiz Page</h2>
      <Grid container spacing={2}>
        <Grid item xs={12}>
          <TextField
            label="Enter your email"
            type="email"
            fullWidth
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
        </Grid>
        {questions.map((question, index) => (
          <Grid item xs={12} key={question.id}>
            <FormControl component="fieldset">
              <FormLabel component="legend">{question.question}</FormLabel>
              {question.type === "Radio" && (
                <RadioGroup
                  onChange={(e) => handleAnswerChange(question.id, e.target.value, true)}
                >
                  {question.options.map((option) => (
                    <FormControlLabel
                      key={option.id}
                      value={option.id}
                      control={<Radio />}
                      label={option.text}
                    />
                  ))}
                </RadioGroup>
              )}
              {question.type === "Checkbox" && (
                <div>
                  {question.options.map((option) => (
                    <FormControlLabel
                      key={option.id}
                      control={<Checkbox />}
                      label={option.text}
                      onChange={(e) => handleAnswerChange(question.id, option.id, e.target.checked)}
                    />
                  ))}
                </div>
              )}
              {question.type === "Textbox" && (
                <TextField
                  label="Your answer"
                  fullWidth
                  onChange={(e) => handleAnswerChange(question.id, e.target.value, true)}
                />
              )}
            </FormControl>
          </Grid>
        ))}
        <Grid item xs={12}>
          <Button variant="contained" onClick={handleSubmit}>Submit Quiz</Button>
        </Grid>
      </Grid>
    </Container>
  );
}

export default QuizPage;
