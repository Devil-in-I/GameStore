import React, { useState } from 'react';
import { Container, Row, Col, Card, Form, Button, Modal } from 'react-bootstrap';
import api from '../api/api';
import { useNavigate } from 'react-router-dom';
import ChangePasswordModal from './ChangePasswordModal';

const EditUserProfilePage = () => {
  const [userName, setUserName] = useState('');
  const [email, setEmail] = useState('');
  const navigate = useNavigate();
  const [showChangePasswordModal, setShowChangePasswordModal] = useState(false);
  const [showSuccessMessage, setShowSuccessMessage] = useState(false);

  const handleUserNameChange = (e) => {
    setUserName(e.target.value);
  };

  const handleEmailChange = (e) => {
    setEmail(e.target.value);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const response = await api.put('/users/current', {
        userName,
        email,
      });
      console.log(response.data);
      alert("Changes are saved.");
      navigate("/profile");
      // Обновление профиля успешно
    } catch (error) {
      console.error(error);
      // Обработка ошибки
    }
  };

  const handleOpenChangePasswordModal = () => {
    setShowChangePasswordModal(true);
  };

  const handleCloseChangePasswordModal = () => {
    setShowChangePasswordModal(false);
    setShowSuccessMessage(true);
  };

  return (
    <Container>
      <Row className="d-flex align-items-center justify-content-center min-vh-100">
        <Col xs={12} md={8} lg={6} className=''>
          <Card style={{ backgroundColor: 'rgb(87 89 94 / 10%)'}}>
            <Card.Body>
              <Card.Title className='mb-5'>Edit Profile</Card.Title>
              <Form onSubmit={handleSubmit}>
                <Form.Group controlId="formUserName" className='mb-3 mt-2'>
                  <Form.Control type="text" value={userName} onChange={handleUserNameChange} />
                  <Form.Label>User Name</Form.Label>
                </Form.Group>
                <Form.Group controlId="formEmail" className='mb-3 mt-2'>
                  <Form.Control type="email" value={email} onChange={handleEmailChange} />
                  <Form.Label>Email</Form.Label>
                </Form.Group>
                <div className="text-center d-flex justify-content-evenly">
                  <Button variant="primary" type="submit" className="mt-3">
                    Save Changes
                    </Button>
                  <Button variant="secondary" className="mt-3 ml-3" onClick={handleOpenChangePasswordModal}>
                    Change Password
                  </Button>
                </div>
              </Form>
            </Card.Body>
          </Card>
        </Col>
      </Row>
      <ChangePasswordModal
        show={showChangePasswordModal}
        onClose={handleCloseChangePasswordModal}
      />
      <Modal show={showSuccessMessage} onHide={() => setShowSuccessMessage(false)} centered>
        <Modal.Body variant="success" className="text-center warning">
          <p>You have successfully changed your password.</p>
        </Modal.Body>
      </Modal>
    </Container>
  );
};

export default EditUserProfilePage;
