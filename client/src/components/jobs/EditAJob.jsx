import { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { Button, Form, FormGroup, Label, Input } from "reactstrap";
import { editJob, getJobById } from "../../managers/jobManager.js";
import "./CreateJob.css"

const EditJob = ({loggedInUser}) => {
    const {jobId} = useParams()
    const [job, setJob] = useState({})
    const [title, setTitle] = useState("");
    const [description, setDescription] = useState("")
    const [minimumRate, setMinimumRate] = useState(null)
    const [maximumRate, setMaximumRate] = useState(0)
    const [numberOfKids, setNumberOfKids] = useState(0)
    const [fullTime, setFullTime] = useState(false)
    const [contactInformation, setContactInformation] = useState("")
    const [PosterId, setPosterId] = useState(null)

    const navigate = useNavigate()

    useEffect(() => {
        if (jobId) {
            const fetchJob = async () => {
                try {
                    const jobData = await getJobById(jobId)
                    setJob(jobData)
                    setTitle(jobData.title)
                    setDescription(jobData.description)
                    setMinimumRate(jobData.payRateMin)
                    setMaximumRate(jobData.payRateMax)
                    setNumberOfKids(jobData.numberOfKids)
                    setFullTime(jobData.fullTime)
                    setContactInformation(jobData.contactInformation)
                    setPosterId(jobData.posterId)
                } catch (error) {
                    console.error("Error fetching this job:", error)
                }
            }

            fetchJob()
        }
    }, [jobId])


      const handleSave = (e) => {
        e.preventDefault();
        const jobData = {
            Title: title,
            Description: description,
            MinimumRate: parseFloat(minimumRate),
            MaximumRate: parseFloat(maximumRate),
            NumberOfKids: numberOfKids,
            FullTime: fullTime,
            ContactInformation: contactInformation,
            PosterId: loggedInUser.id
        }

        console.log('Job Data:', jobData)
        
        try {
          const response = editJob(jobData, parseInt(jobId));
          console.log('Response:', response);  // Debug logging
          navigate(`/jobs/${jobId}`)
        } catch (error) {
          console.error("There was an error saving the job!", error);
        }
      };

      const handleCancel = () => {
        navigate(`/jobs/${jobId}`);
      };

      return (
        <>
          <h2>Edit Job</h2>
          <Form onSubmit={handleSave}>
            <FormGroup>
              <Label>Title</Label>
              <Input
                type="text"
                value={title}
                onChange={(e) => {
                  setTitle(e.target.value);
                }}
              />
            </FormGroup>
            <FormGroup>
              <Label>Description</Label>
              <Input
                type="text"
                value={description}
                onChange={(e) => {
                  setDescription(e.target.value);
                }}
              />
            </FormGroup>
            <FormGroup>
              <Label>Minimum Rate</Label>
              <Input
                type="number"
                step="0.01" // Allow decimals
                value={minimumRate}
                onChange={(e) => {
                    setMinimumRate(e.target.value);
                }}
                />
            </FormGroup>
            <FormGroup>
              <Label>Maximum Rate</Label>
              <Input
                        type="number"
                        step="0.01" // Allow decimals
                        value={maximumRate}
                        onChange={(e) => {
                            setMaximumRate(e.target.value);
                        }}
                    />
            </FormGroup>
            <FormGroup>
              <Label>Number Of Kids</Label>
              <Input
                        type="select"
                        value={numberOfKids}
                        onChange={(e) => {
                            setNumberOfKids(e.target.value);
                        }}
                    >
                        <option value="1">1</option>
                        <option value="2">2</option>
                        <option value="3">3</option>
                        <option value="4">4</option>
                        <option value="5">5</option>
                        <option value="6">6</option>
                        <option value="7">7</option>
                        <option value="8">8</option>
                        <option value="9">9</option>
                        <option value="10">10</option>

                    </Input>
            </FormGroup>
            <FormGroup check>
                <Label check>
                    <Input
                        type="checkbox"
                        checked={fullTime}
                        onChange={(e) => {
                            setFullTime(e.target.checked);
                        }}
                    />
                    {' '}
                    Full Time
                </Label>
            </FormGroup>
            <FormGroup>
              <Label>Contact Information</Label>
              <Input
                type="text"
                value={contactInformation}
                onChange={(e) => {
                  setContactInformation(e.target.value);
                }}
              />
            </FormGroup>
            <Button color="primary" type="submit">
                Save
            </Button>
            <Button color="secondary" onClick={handleCancel}>
                Cancel
            </Button>
          </Form>
        </>
      );
}

export default EditJob
