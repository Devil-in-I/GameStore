import React, { useEffect, useState } from 'react';
import { Row, Col } from 'react-bootstrap';
import GameCard from './GameCard';
import api from '../api/api';
import { variables } from '../Variables';

const GamesRow = ({ games, isStorePage }) => {
  const [userGames, setUserGames] = useState([]);

  useEffect(() => {
    const fetchUserGames = async () => {
      try {
        const response = await api.get(`${variables.API_URI}Users/current/games`);
        setUserGames(response.data.map(game => game.id));
      } catch (error) {
        // Обработка ошибки
      }
    };

    fetchUserGames();
  }, []);

  return (
    <Row xs={1} md={2} lg={3} xl={4} xxl={5} className="g-4">
      {games.map(game => (
        <Col key={game.id}>
          <GameCard
            game={game}
            isStorePage={isStorePage}
            isOwnedByUser={userGames.includes(game.id)}
          />
        </Col>
      ))}
    </Row>
  );
};

export default GamesRow;