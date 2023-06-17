import React, { useState } from 'react';
import { Modal, Form, Button } from 'react-bootstrap';
import api from '../api/api';

const ChangePasswordModal = ({ show, onClose }) => {
  const [currentPassword, setCurrentPassword] = useState('');
  const [newPassword, setNewPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [error, setError] = useState(null);
  const [email, setEmail] = useState('');

  const handleSave = async () => {
    try {
      const response = await api.post('/Users/change-password', {
        email,
        currentPassword,
        newPassword,
      });
      if (response.status === 200) {
        onClose(); // Закрыть модальное окно после успешного сохранения
      } else {
        setError('Something went wrong. Please try again.'); // Обработка неудачного ответа от API
      }
    } catch (error) {
      setError('Something went wrong. Please try again.'); // Обработка ошибки при выполнении запроса
    }
  };

  const handleConfirmPasswordChange = (e) => {
    setConfirmPassword(e.target.value);
  };

  const handleEmailChange = (e) => {
    setEmail(e.target.value);
  };
  
  const isPasswordMatch = newPassword === confirmPassword;

  return (
    <Modal show={show} onHide={onClose} centered>
      <Modal.Header closeButton>
        <Modal.Title>Change Password</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        {error && <div className="error-message">{error}</div>}
        <Form>
        <Form.Group controlId="formEmail">
          <Form.Label>Email</Form.Label>
          <Form.Control
            type="email"
            placeholder="Enter email"
            value={email}
            onChange={handleEmailChange}
          />
        </Form.Group>
          <Form.Group controlId="formCurrentPassword">
            <Form.Label>Current Password</Form.Label>
            <Form.Control
              type="password"
              placeholder="Enter current password"
              value={currentPassword}
              onChange={(e) => setCurrentPassword(e.target.value)}
            />
          </Form.Group>
          <Form.Group controlId="formNewPassword">
            <Form.Label>New Password</Form.Label>
            <Form.Control
              type="password"
              placeholder="Enter new password"
              value={newPassword}
              onChange={(e) => setNewPassword(e.target.value)}
            />
          </Form.Group>
          <Form.Group controlId="formConfirmPassword">
            <Form.Label>Confirm Password</Form.Label>
            <Form.Control
              type="password"
              placeholder="Confirm new password"
              value={confirmPassword}
              onChange={handleConfirmPasswordChange}
              isInvalid={!isPasswordMatch}
            />
            {!isPasswordMatch && <Form.Control.Feedback type="invalid">Passwords do not match</Form.Control.Feedback>}
          </Form.Group>
        </Form>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={onClose}>
          Cancel
        </Button>
        <Button variant="primary" onClick={handleSave} disabled={!isPasswordMatch}>
          Save
        </Button>
      </Modal.Footer>
    </Modal>
  );
};

export default ChangePasswordModal;