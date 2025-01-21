import { useState, useEffect } from 'react';
import axios from 'axios';


const exampleHighScores = [
  { email: 'user1@example.com', score: 150, dateTime: '2025-01-20T10:00:00Z' },
  { email: 'user2@example.com', score: 130, dateTime: '2025-01-19T15:30:00Z' },
  { email: 'user3@example.com', score: 120, dateTime: '2025-01-18T08:45:00Z' },
  { email: 'user4@example.com', score: 100, dateTime: '2025-01-17T12:00:00Z' },
  { email: 'user5@example.com', score: 90, dateTime: '2025-01-16T10:30:00Z' },
  { email: 'user6@example.com', score: 80, dateTime: '2025-01-15T09:00:00Z' },
  { email: 'user7@example.com', score: 70, dateTime: '2025-01-14T11:15:00Z' },
  { email: 'user8@example.com', score: 60, dateTime: '2025-01-13T16:45:00Z' },
  { email: 'user9@example.com', score: 50, dateTime: '2025-01-12T14:00:00Z' },
  { email: 'user10@example.com', score: 40, dateTime: '2025-01-11T13:30:00Z' },
];

const useHighScores = () => {
  const [highScores, setHighScores] = useState([]);

  useEffect(() => {
    const fetchHighScores = async () => {
      try {
        const response = await axios.get('http://localhost:5234/api/highscores');
        if (Array.isArray(response.data) && response.data.length > 0) {
          setHighScores(response.data);
        } else {
          console.log('No high scores from API, using example data.');
          setHighScores(exampleHighScores);
        }
      } catch (error) {
        console.error("Error fetching high scores:", error);
        setHighScores(exampleHighScores);
      }
    };

    fetchHighScores();
  }, []);

  return highScores;
};

export default useHighScores;
