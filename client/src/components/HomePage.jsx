import React from 'react';
import { Outlet } from 'react-router-dom';

const HomePage = () => {
  return (
    <div>
      <h1>Welcome to NannyNook!</h1>
      <Outlet />
    </div>
  );
};

export default HomePage;
