import React, { useState } from 'react';
import { Form, Button, Container } from 'react-bootstrap';
import axios from 'axios';
import { variables } from '../Variables';
import { useNavigate } from 'react-router-dom';

const RegistrationPage = () => {
  const [formData, setFormData] = useState({
    name: '',
    email: '',
    password: '',
    confirmPassword: ''
  });
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [passwordError, setPasswordError] = useState(false);
  const navigate = useNavigate();

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
    setPasswordError(formData.password !== e.target.value);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (formData.password !== formData.confirmPassword) {
      setPasswordError(true);
      return;
    }

    setIsSubmitting(true);
    const payload = {
      user: {
        username: formData.name,
        email: formData.email
      },
      password: formData.password
    };
  
    axios.post(`${variables.API_URI}Users/register`, payload)
      .then(response => {
        // handle success response
        navigate('/account/login');
      })
      .catch(error => {
        console.error('Error registering user:', error);
      })
      .finally(() => {
        setIsSubmitting(false);
      });  
  };

  return (
    <Container className="d-flex justify-content-center align-items-center min-vh-100">
      <div className="bg-dark p-4 rounded">
        <h1 className="text-center mb-4">Registration</h1>
        <Form onSubmit={handleSubmit}>
          <Form.Group controlId="formBasicName">
            <Form.Label>Name</Form.Label>
            <Form.Control type="text" placeholder="Enter name" name="name" onChange={handleChange} required />
          </Form.Group>
          <Form.Group controlId="formBasicEmail">
            <Form.Label>Email address</Form.Label>
            <Form.Control type="email" placeholder="Enter email" name="email" onChange={handleChange} required />
          </Form.Group>
          <Form.Group controlId="formBasicPassword">
            <Form.Label>Password</Form.Label>
            <Form.Control
              type="password"
              placeholder="Password"
              name="password"
              onChange={handleChange}
              required
            />
          </Form.Group>
          <Form.Group controlId="formBasicConfirmPassword">
            <Form.Label>Confirm Password</Form.Label>
            <Form.Control
              type="password"
              placeholder="Confirm Password"
              name="confirmPassword"
              onChange={handleChange}
              required
              isInvalid={passwordError}
            />
            <Form.Control.Feedback type="invalid">
              {passwordError && 'Passwords do not match'}
            </Form.Control.Feedback>
          </Form.Group>
          <Button className='mt-3' variant="warning" type="submit" disabled={isSubmitting} block>
            {isSubmitting ? 'Registering...' : 'Register'}
          </Button>
        </Form>
      </div>
    </Container>
  );
};

export default RegistrationPage;
