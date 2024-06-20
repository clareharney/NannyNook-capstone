import React, { useState } from 'react';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import { useNavigate } from 'react-router-dom';
import { createJob } from '../../managers/jobManager.js';
import './CreateJob.css';

const CreateJob = ({ loggedInUser }) => {
  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');
  const [minimumRate, setMinimumRate] = useState(0);
  const [maximumRate, setMaximumRate] = useState(0);
  const [numberOfKids, setNumberOfKids] = useState(1);
  const [fullTime, setFullTime] = useState(false);
  const [contactInformation, setContactInformation] = useState('');
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    const jobData = {
      Title: title,
      Description: description,
      PayRateMin: parseFloat(minimumRate),
      PayRateMax: parseFloat(maximumRate),
      NumberOfKids: parseInt(numberOfKids),
      FullTime: fullTime,
      ContactInformation: contactInformation,
      PosterId: loggedInUser.id,
    };

    console.log('Job Data:', jobData);

    try {
      const createdJob = await createJob(jobData);
      console.log('Response:', createdJob); // Debug logging
      navigate(`/jobs/${createdJob.id}`);
    } catch (error) {
      console.error('There was an error creating the job!', error);
    }
  };

  const handleCancel = () => {
    navigate('/jobs');
  };

  return (
    <div className="form-container">
      <h2 className="form-title">Create A Job Posting</h2>
      <Form onSubmit={handleSubmit}>
        <FormGroup className="form-group">
          <Label className="form-label">Title</Label>
          <Input
            type="text"
            value={title}
            onChange={(e) => setTitle(e.target.value)}
            className="form-input"
          />
        </FormGroup>
        <FormGroup className="form-group">
          <Label className="form-label">Description</Label>
          <Input
            type="text"
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            className="form-input"
          />
        </FormGroup>
        <FormGroup className="form-group">
          <Label className="form-label">Minimum Rate</Label>
          <Input
            type="number"
            step="0.01"
            value={minimumRate}
            onChange={(e) => setMinimumRate(e.target.value)}
            className="form-input"
          />
        </FormGroup>
        <FormGroup className="form-group">
          <Label className="form-label">Maximum Rate</Label>
          <Input
            type="number"
            step="0.01"
            value={maximumRate}
            onChange={(e) => setMaximumRate(e.target.value)}
            className="form-input"
          />
        </FormGroup>
        <FormGroup className="form-group">
          <Label className="form-label">Number Of Kids</Label>
          <Input
            type="select"
            value={numberOfKids}
            onChange={(e) => setNumberOfKids(e.target.value)}
            className="form-input"
          >
            {[...Array(10)].map((_, index) => (
              <option key={index + 1} value={index + 1}>
                {index + 1}
              </option>
            ))}
          </Input>
        </FormGroup>
        <FormGroup check className="form-group">
          <Label check className="form-label">
            <Input
              type="checkbox"
              checked={fullTime}
              onChange={(e) => setFullTime(e.target.checked)}
            />{' '}
            Full Time
          </Label>
        </FormGroup>
        <FormGroup className="form-group">
          <Label className="form-label">Contact Information</Label>
          <Input
            type="text"
            value={contactInformation}
            onChange={(e) => setContactInformation(e.target.value)}
            className="form-input"
          />
        </FormGroup>
        <Button type="submit" className="form-submit-button">
          Submit
        </Button>
        <Button color="secondary" onClick={handleCancel} className="form-submit-button">
          Cancel
        </Button>
      </Form>
    </div>
  );
};

export default CreateJob;
