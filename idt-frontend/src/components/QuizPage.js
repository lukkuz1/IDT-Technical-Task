import React from "react";
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
import useQuiz from "../hooks/useQuiz";


const RadioQuestion = ({ question, value, onChange }) => (
  <RadioGroup value={value} onChange={onChange}>
    {question.options.map((option) => (
      <FormControlLabel key={option.id} value={option.id} control={<Radio />} label={option.text} />
    ))}
  </RadioGroup>
);

const CheckboxQuestion = ({ question, value, onChange }) => (
  <div>
    {question.options.map((option) => (
      <FormControlLabel
        key={option.id}
        control={<Checkbox checked={value.includes(option.id)} onChange={(e) => onChange(option.id, e.target.checked)} />}
        label={option.text}
      />
    ))}
  </div>
);

const TextboxQuestion = ({ value, onChange }) => (
  <TextField label="Your answer" fullWidth value={value} onChange={onChange} />
);

function QuizPage() {
  const {
    questions,
    email,
    setEmail,
    answers,
    handleAnswerChange,
    score,
    handleSubmit,
  } = useQuiz();
  
  const navigate = useNavigate();

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
              <RadioQuestion
                question={question}
                value={answers[question.id]?.selectedOptions[0]?.id || ""}
                onChange={(e) => handleAnswerChange(question.id, e.target.value)}
              />
            )}

            {question.type === 1 && (
              <CheckboxQuestion
                question={question}
                value={answers[question.id]?.selectedOptions.map((o) => o.id) || []}
                onChange={(value, isChecked) => handleAnswerChange(question.id, value, isChecked)}
              />
            )}

            {question.type === 2 && (
              <TextboxQuestion
                value={answers[question.id]?.textAnswer || ""}
                onChange={(e) => handleAnswerChange(question.id, e.target.value)}
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
        <Button variant="contained" color="secondary" onClick={() => navigate("/highscores")}>
          View High Scores
        </Button>
      </Box>
    </Container>
  );
}

export default QuizPage;
