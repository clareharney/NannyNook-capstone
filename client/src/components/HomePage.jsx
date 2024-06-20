import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { getOccasionByNotUserId } from '../managers/occasionManager.js';
import { Button, Card, CardBody, CardSubtitle, CardText, CardTitle } from 'reactstrap';
import './HomePage.css';

const HomePage = ({ loggedInUser }) => {
  const [occasions, setOccasions] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    getOccasionByNotUserId(loggedInUser.id).then((arr) => setOccasions(arr));
  }, [loggedInUser.id]);

  return (
    <div className="container">
      <h1>Events List</h1>
      <div className="grid-container">
        {occasions.map((o) => (
          <Card key={o.id} className="grid-item">
            <CardBody>
              <CardTitle tag="h5">{o.title}</CardTitle>
              <CardSubtitle className="mb-2 text-muted" tag="h6">
                {o.hostUserProfile.fullName}
              </CardSubtitle>
              <CardText>{o.category?.name}</CardText>
              <CardText>{o.location}</CardText>
              <CardText>{o.formattedDate}</CardText>
              <CardBody>{o.description}</CardBody>
              <Button onClick={() => navigate(`/events/${o.id}`)}>
                View Event
              </Button>
            </CardBody>
          </Card>
        ))}
      </div>
    </div>
  );
};

export default HomePage;

