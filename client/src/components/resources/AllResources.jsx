import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Button, Card, CardBody, CardText, CardTitle } from 'reactstrap';
import { getResources } from '../../managers/resourceManager.js';
import './AllResources.css';

const AllResources = () => {
  const [resources, setResources] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    getResources().then((arr) => setResources(arr));
  }, []);

  return (
    <div className="container">
      <h1>Resources For Caregivers</h1>
      <div className="grid-container">
        {resources.map((r) => (
          <Card key={r.id} className="grid-item">
            <CardBody>
              <CardTitle>{r.title}</CardTitle>
              <CardText>{r.type}</CardText>
              <CardText>{r.author}</CardText>
              <Button onClick={() => navigate(`/resources/${r.id}`)}>
                View Resource
              </Button>
            </CardBody>
          </Card>
        ))}
      </div>
    </div>
  );
};

export default AllResources;
