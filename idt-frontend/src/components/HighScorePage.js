import React from 'react';
import { Container, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography, Paper } from '@mui/material';
import useHighScores from '../hooks/useHighScores';


const Medal = ({ position }) => {
  const getMedalIcon = () => {
    if (position === 1) return '🥇'; // Gold
    if (position === 2) return '🥈'; // Silver
    if (position === 3) return '🥉'; // Bronze
    return '';
  };

  return <span>{getMedalIcon()}</span>;
};


const HighScoreRow = ({ index, score }) => {
  return (
    <TableRow>
      <TableCell align="center">
        <Medal position={index + 1} /> {index + 1}
      </TableCell>
      <TableCell align="center">{score.email}</TableCell>
      <TableCell align="center">{score.score}</TableCell>
      <TableCell align="center">{new Date(score.dateTime).toLocaleString()}</TableCell>
    </TableRow>
  );
};

function HighScorePage() {
  const highScores = useHighScores();

  return (
    <Container>
      <Typography variant="h4" gutterBottom>High Scores</Typography>
      <TableContainer component={Paper}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell align="center">Position</TableCell>
              <TableCell align="center">Email</TableCell>
              <TableCell align="center">Score</TableCell>
              <TableCell align="center">Date</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {highScores.length > 0 ? (
              highScores.map((score, index) => (
                <HighScoreRow key={index} index={index} score={score} />
              ))
            ) : (
              <TableRow>
                <TableCell colSpan={4} align="center">
                  <Typography>No high scores available.</Typography>
                </TableCell>
              </TableRow>
            )}
          </TableBody>
        </Table>
      </TableContainer>
    </Container>
  );
}

export default HighScorePage;
