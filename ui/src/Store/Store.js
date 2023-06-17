import React, { useState, useEffect, useCallback } from 'react';
import axios from 'axios';
import GamesRow from './GamesRow';
import { variables } from '../Variables';

const Store = () => {
  const [games, setGames] = useState([]);

  const refreshList = useCallback(() => {
    axios
      .get(variables.API_URI + 'Games')
      .then(response => {
        setGames(response.data);
      })
      .catch(error => {
        console.error(error);
      });
  }, []);

  useEffect(() => {
    refreshList();
  }, [refreshList]);

  return (
    <div className="store-container rounded">
      {games.length > 0 ? (
        <GamesRow games={games} isStorePage={true} />
      ) : (
        <div style={{ textAlign: 'center', color: 'white', marginTop: '50%' }}>
          Games not available.
        </div>
      )}
    </div>
  );
};

export default Store;
