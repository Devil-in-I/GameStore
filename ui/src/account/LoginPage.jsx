import React, { useState } from 'react';
import { Form, Button, Container, Row, Col, Modal, Alert } from 'react-bootstrap';
import { Link, useNavigate } from 'react-router-dom';
import axios from 'axios';
import { variables } from '../Variables';

const LoginPage = () => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [showMessage, setShowMessage] = useState(false);
  const navigate = useNavigate();

  const handleSubmit = (event) => {
    event.preventDefault();
    axios
      .post(`${variables.API_URI}Users/login`, {
        username: username,
        password: password,
      })
      .then((response) => {
        console.log(response);
        localStorage.removeItem('authToken');
        localStorage.setItem('authToken', response.data.token);
        console.log(response.data.token);
        setShowMessage(true);
        setTimeout(() => {
          setShowMessage(false);
          navigate('/store');
        }, 1500);
      })
      .catch((error) => {
        setError(error.message);
      });
  };

  return (
    <Container className="login-container d-flex justify-content-center align-items-center min-vh-100">
      <Col md={6} lg={4}>
        <h1 className="text-center mb-4">Login</h1>
        {error && <Alert variant="warning">Invalid userName or password. Please, try again!</Alert>}
        <Form onSubmit={handleSubmit}>
          <Form.Group controlId="formEmail">
            <Form.Label>Username</Form.Label>
            <Form.Control
              type="text"
              placeholder="Enter username"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
              required
            />
          </Form.Group>
          <Form.Group controlId="formPassword">
            <Form.Label>Password</Form.Label>
            <Form.Control
              type="password"
              placeholder="Password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
            />
          </Form.Group>
          <Button variant="primary" type="submit" block className="mt-3">
            Login
          </Button>
        </Form>
        <p className="text-center mt-3">
          Don't have an account yet? <Link to="/account/register">Register</Link>
        </p>
      </Col>
      <Modal show={showMessage} onHide={() => setShowMessage(false)} centered>
        <Modal.Body variant="success" className='text-center warning'>
          <p>You're successfully logged in to Black Mirror.</p>
        </Modal.Body>
      </Modal>
    </Container>
  );
};

export default LoginPage;