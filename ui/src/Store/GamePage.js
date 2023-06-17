import React, { useEffect, useState } from 'react';
import { Card, Button, Badge, Container, Row, Col } from 'react-bootstrap';
import { ShoppingCartOutlined } from '@ant-design/icons';
import { variables } from '../Variables';
import defaultImg from '../assets/defaultImg.jpg';
import { useLocation, useParams, useNavigate } from 'react-router-dom';
import api from '../api/api';

const GamePage = () => {
  const { id } = useParams();
  const { isOwnedByUser } = useLocation();
  const [gameData, setGameData] = useState(null);
  const [showPurchaseMessage, setShowPurchaseMessage] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchGameData = async () => {
      try {
        console.log(isOwnedByUser);
        const response = await api.get(`${variables.API_URI}Games/${id}`);
        setGameData(response.data);
      } catch (error) {
        console.error(error);
      }
    };

    fetchGameData();
  }, [id]);

  const handleBuyNowClick = async () => {
    try {
      const response = await api.put(`${variables.API_URI}Users/current/games/add/${id}`);
      setShowPurchaseMessage(true);
      console.log(response.data);
      setTimeout(() => {
        setShowPurchaseMessage(false);
        navigate('/library'); // Редирект происходит после исчезновения окна
      }, 3000); // Через 3 секунды скрываем сообщение и выполняем редирект
    } catch (error) {
      console.error(error);
    }
  };
  
  if (!gameData) {
    return <div>Loading...</div>;
  }

  return (
    <div className="rounded" style={{ paddingTop: '40px', backgroundColor: 'rgb(27, 28, 31)' }}>
      {showPurchaseMessage && (
        <div
          className="purchase-message"
          style={{
            position: 'fixed',
            top: 0,
            left: 0,
            width: '100%',
            padding: '10px',
            backgroundColor: 'green',
            color: 'white',
            textAlign: 'center',
            zIndex: 9999,
          }}
        >
          Game purchased successfully!
        </div>
      )}
      <Container>
        <Row className="justify-content-center align-items-center">
          <Col xs={12} md={6}>
            <Card className="p-4" style={{ backgroundColor: 'transparent', border: 'none' }}>
              <Row>
                <Col xs={12} md={6} className="mb-3 mb-md-0">
                  <Card.Img
                    className="photo"
                    variant="top"
                    src={gameData.imageUrl ? gameData.imageUrl : defaultImg}
                    alt={gameData.name}
                    style={{ width: '100%', objectFit: 'cover', borderRadius: '4px' }}
                  />
                </Col>
                <Col xs={12} md={6}>
                  <Card.Title as="h1" style={{ fontSize: '36px', marginBottom: '10px', color: 'white' }}>
                    {gameData.name}
                  </Card.Title>
                  <div className="mb-3">
                    <h2 className="mb-2" style={{ fontSize: '24px', color: 'white' }}>
                      Genres:
                    </h2>
                    <div style={{ marginBottom: '10px' }}>
                      {gameData.genres.map((genre) => (
                        <Badge
                          key={genre.id}
                          variant="light"
                          className="mr-2"
                          style={{ fontWeight: 'bold', marginRight: '5px' }}
                        >
                          {genre.name}
                        </Badge>
                      ))}
                    </div>
                    <p style={{ fontSize: '24px', marginBottom: '10px', color: 'white' }}>
                      Price: {gameData.price ? `${gameData.price} USD` : 'N/A'}
                    </p>
                    {isOwnedByUser ? (
                      <Button variant="primary" size="lg" style={{ fontSize: '24px', width: '200px' }}>
                        Show in Library
                      </Button>
                    ) : (
                      <Button
                        variant="primary"
                        size="lg"
                        style={{ fontSize: '24px', width: '200px' }}
                        onClick={handleBuyNowClick}
                      >
                        <ShoppingCartOutlined /> Buy Now
                      </Button>
                    )}
                  </div>
                </Col>
              </Row>
            </Card>
          </Col>
        </Row>
      </Container>
      <hr style={{ margin: '18px 0' }} />
      <h2 style={{ color: 'white', fontSize: '36px', marginBottom: '20px', textAlign: 'center' }}>Description</h2>
      <Container>
        <Row>
          <Col>
            <Card.Text
              as="p"
              className="text-secondary description-bg"
              style={{ fontSize: '18px', marginBottom: '20px', color: 'white' }}
            >
              {gameData.description ? gameData.description : 'No description available for this game.'}
            </Card.Text>
          </Col>
        </Row>
      </Container>
    </div>
  );
};

export default GamePage;