import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Container, List, ListItem, ListItemText, Typography } from '@mui/material';

function HighScorePage() {
  const [highScores, setHighScores] = useState([]);

  useEffect(() => {
    axios.get('http://localhost:5234/api/highscores')
      .then(response => {
        if (Array.isArray(response.data)) {
          setHighScores(response.data);
        } else {
          console.error('Unexpected response data structure:', response.data);
        }
      })
      .catch(error => console.error("Error fetching high scores:", error));
  }, []);

  const getColorForPosition = (position) => {
    if (position === 1) return 'gold';
    if (position === 2) return 'silver';
    if (position === 3) return 'bronze';
    return 'inherit';
  };

  return (
    <Container>
      <Typography variant="h4" gutterBottom>High Scores</Typography>
      <List>
        {Array.isArray(highScores) && highScores.length > 0 ? (
          highScores.map((score, index) => (
            <ListItem key={index}>
              <ListItemText
                primary={
                  <span style={{ color: getColorForPosition(score.position) }}>
                    {score.position}. {score.email} - {score.score}
                  </span>
                }
                secondary={`Date: ${new Date(score.dateTime).toLocaleString()}`}
              />
            </ListItem>
          ))
        ) : (
          <Typography>No high scores available.</Typography>
        )}
      </List>
    </Container>
  );
}

export default HighScorePage;