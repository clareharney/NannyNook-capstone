import React from 'react';
import './WelcomeMessage.css'; // Import the CSS file for styling

const WelcomeMessage = () => {
  return (
    <div className="welcome-container">
      <h1 className="welcome-title">Welcome To NannyNook!</h1>
      <p className="welcome-text">Nannies work REALLY hard. Why can't we play hard, too??</p>
      <p className="welcome-text">Welcome to your online playground</p>
    </div>
  );
};

export default WelcomeMessage;
