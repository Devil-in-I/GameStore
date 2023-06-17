import React from 'react';
import { Row, Col } from 'react-bootstrap';
import blackMirrorLogo from '../assets/logos/logo_nobg2.png';

const Home = () => {
  return (
    <div className="bg-dark d-flex flex-column justify-content-center align-items-center rounded mt-2" style={{ minHeight: '88vh' }}>

      <img src={blackMirrorLogo} alt="Black Mirror Logo" className="" style={{ width: '400px' }} />
      <div className="text-center text-white">
        <h2 className="mb-4">Welcome to the Dark Side</h2>
        <p className="mb-3">
          Explore the eerie world of Black Mirror and uncover the hidden mysteries of technology and society.
        </p>
        <p className="mb-3">
          Immerse yourself in mind-bending stories that will make you question the impact of technology on our lives.
        </p>
        <p className="mb-3">
          Get ready to dive into the twisted and thought-provoking realm of Black Mirror.
        </p>
      </div>
      <div className="mt-5">
        <a href="/store" className="btn btn-light btn-lg">Enter the Store</a>
      </div>
    </div>
  );
};

export default Home;
