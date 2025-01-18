// src/components/HighScorePage.js
import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Container, List, ListItem, ListItemText, Typography } from '@mui/material';

function HighScorePage() {
  const [highScores, setHighScores] = useState([]);

  useEffect(() => {
    // Fetch the top 10 high scores from the backend
    axios.get('http://localhost:5000/api/highscores')
      .then(response => {
        setHighScores(response.data);
      })
      .catch(error => {
        console.error("There was an error fetching the high scores!", error);
      });
  }, []);

  return (
    <Container>
      <h2>High Scores</h2>
      <List>
        {highScores.map((score, index) => (
          <ListItem key={index}>
            <ListItemText
              primary={`${score.position}. ${score.email} - ${score.score}`}
              secondary={`Date: ${new Date(score.dateTime).toLocaleString()}`}
            />
          </ListItem>
        ))}
      </List>
    </Container>
  );
}

export default HighScorePage;
