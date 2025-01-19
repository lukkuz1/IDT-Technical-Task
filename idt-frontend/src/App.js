import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import QuizPage from './components/QuizPage';
import HighScorePage from './components/HighScorePage';

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<QuizPage />} />
        <Route path="/highscores" element={<HighScorePage />} />
      </Routes>
    </Router>
  );
}

export default App;
