import React from 'react';
import { Route, NavLink, Routes, useNavigate } from 'react-router-dom';
import { BiLogOut } from 'react-icons/bi';
import logo from './assets/logos/logo_nobg.png';
import { Navbar, Nav, NavDropdown } from 'react-bootstrap';

import Home from './Home/Home';
import Store from './Store/Store';
import GamePage from './Store/GamePage';
import LibraryPage from './library/LibraryPage';
import UserProfilePage from './profile/UserProfilePage';
import './App.css';
import EditUserProfilePage from './profile/EditUserProfilePage';
import RegistrationPage from './account/RegistrationPage';
import LoginPage from './account/LoginPage';

function App() {
  const navigate = useNavigate();

  const handleLogout = () => {
    localStorage.removeItem('authToken'); // Удаление jwt-токена из локального хранилища
    navigate('/account/login'); // Перенаправление на страницу входа
  };

  return (
    <div className="App container">
      <header>
        <Navbar expand="sm" variant="dark" className="mt-3 px-3" style={{ backgroundColor: '#1b1c1f', borderRadius: '10px' }}>
          <Navbar.Brand className="mx-auto">
            <NavLink className="navbar-brand d-flex align-items-center" to="/home">
              <img src={logo} alt="Logo" className="App-logo" style={{ width: '55px', height: '55px' }} />
              <span className="brand-name">Black Mirror</span>
            </NavLink>
          </Navbar.Brand>
          <Navbar.Toggle aria-controls="navbar-nav" />
          <Navbar.Collapse id="navbar-nav">
            <Nav className="ml-auto d-flex align-items-center">
              <NavLink className="nav-link" to="/home">
                Home
              </NavLink>
              <NavLink className="nav-link" to="/store">
                Store
              </NavLink>
              <NavLink className="nav-link" to="/library">
                My games
              </NavLink>
              <NavLink className="nav-link" to="/profile">
                My profile
              </NavLink>
              <Nav.Item>
                <button className="btn logout btn-link nav-link" onClick={handleLogout}>
                  <BiLogOut />
                </button>
              </Nav.Item>
            </Nav>
          </Navbar.Collapse>
        </Navbar>
      </header>
      <div className="rounded main-content mb-2 mt-2">
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/home" element={<Home />} />
          <Route path="/store">
            <Route index element={<Store />} />
            <Route path="game/:id" element={<GamePage />} />
          </Route>
          <Route path="/library" element={<LibraryPage />} />
          <Route path="/profile">
            <Route index element={<UserProfilePage />} />
            <Route path="edit" element={<EditUserProfilePage />} />
          </Route>
          <Route path="/account">
            <Route path="login" element={<LoginPage />} />
            <Route path="register" element={<RegistrationPage />} />
          </Route>
        </Routes>
      </div>
    </div>
  );
}

export default App;