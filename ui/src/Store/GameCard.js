import React from 'react';
import { Card, Button, Badge } from 'react-bootstrap';
import { ShoppingCartOutlined } from '@ant-design/icons';
import { Link } from 'react-router-dom';
import defaultImg from '../assets/defaultImg.jpg';

const GameCard = ({ game, isStorePage, isOwnedByUser }) => {
  return (
    <Link to={{ pathname: `/store/game/${game.id}`, state: {isOwnedByUser: isOwnedByUser} }} className="text-decoration-none">
      <Card className="cursor-pointer transition-shadow" style={{ boxShadow: '0 0 10px gold', backgroundColor: 'rgb(87 89 94 / 43%)', color: 'white', minHeight:'433px'}}>
        <Card.Img variant="top" src={game.imageUrl ? game.imageUrl : defaultImg} alt={game.title} />
        <Card.Body>
          <Card.Title>{game.name}</Card.Title>
          <div className="mb-2">
            <span>Genres:</span>
            {game.genres.map(genre => (
              <Badge style={{ marginRight: '5px' }} key={genre.id} className="mr-1">{genre.name}</Badge>
            ))}
          </div>
          {isStorePage && (
            <div className="d-flex justify-content-center">
              <div>
                <span>Price: {game.price ? `${game.price} USD` : 'N/A'}</span>
              </div>
            </div>
          )}
          <div className="d-flex justify-content-center mt-5">
            {isOwnedByUser ? (
              <Button variant="warning" size="sm" className="rounded" icon={<ShoppingCartOutlined />}>
                See details
              </Button>
            ) : (
              <Button variant="warning" size="sm" className="rounded" icon={<ShoppingCartOutlined />}>
                Buy
              </Button>
            )}
          </div>
        </Card.Body>
      </Card>
    </Link>
  );
};

export default GameCard;
