import React, { useEffect, useState } from 'react';
import { Container } from 'react-bootstrap';
import GamesRow from '../Store/GamesRow';
import api from '../api/api';
import { variables } from '../Variables';
import defaultImg from '../assets/defaultImg.jpg';
import { useNavigate } from 'react-router-dom';

const LibraryPage = () => {
  const [userGames, setUserGames] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchUserGames = async () => {
      try {
        const response = await api.get(`${variables.API_URI}Users/current/games`);
        setUserGames(response.data);
      } catch (error) {
        if (error.message === 'Unauthorized') {
          navigate('/account/login');
        }
      }
    };

    fetchUserGames();
  }, []);

  return (
    <div className="rounded" style={{ paddingTop: '40px', backgroundColor: 'rgb(27, 28, 31)', minHeight: '90vh' }}>
      <Container>
        {userGames.length > 0 ? (
          <GamesRow games={userGames} isStorePage={false} />
        ) : (
          <div style={{ textAlign: 'center', color: 'white' }}>No games in the library.</div>
        )}
      </Container>
    </div>
  );
};

export default LibraryPage;