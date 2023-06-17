import React, { useEffect, useState } from 'react';
import { Container, Row, Col, Card, Button } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import defaultProfileImage from '../assets/profilePhotos/DefaultProfileImage2.jpg';
import { variables } from '../Variables';
import api from '../api/api';

const UserProfilePage = () => {
  const [userData, setUserData] = useState(null);

  useEffect(() => {
    const fetchUserData = async () => {
      try {
        const response = await api.get(`${variables.API_URI}Users/current`);
        setUserData(response.data);
      } catch (error) {
        console.error(error);
      }
    };

    fetchUserData();
  }, []);

  if (!userData) {
    return <div>Loading...</div>;
  }


  return (
    <Container>
      <Row className="justify-content-center">
        <Col xs={12} md={8} lg={6}>
          <Card style={{ backgroundColor: 'inherit', border: 'none', padding: '10px' }}>
            <Card.Img variant="top" src={defaultProfileImage} alt="Profile" />
            <Card.Body>
              <Card.Title>User Profile</Card.Title>
              <Card.Text>
                <strong>Username:</strong> {userData.userName}
              </Card.Text>
              <Card.Text>
                <strong>Email:</strong> {userData.email}
              </Card.Text>
              <Link to={`/profile/edit`}>
                <Button variant="primary">Edit Profile</Button>
              </Link>
            </Card.Body>
          </Card>
        </Col>
      </Row>
    </Container>
  );
};

export default UserProfilePage;