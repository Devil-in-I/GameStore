import { createGlobalStyle } from 'styled-components';
import background from '../assets/BackgroundImages/v2/background5.jpg';

const GlobalStyles = createGlobalStyle`
  /* Общие стили для всего приложения */
  body {
    margin: 0;
    padding: 0;
    color: #C5C6D0;
    background-image: url("${background}");
    background-repeat: no-repeat;
    background-size: cover;
    background-position: center;
  }
  
  .photo {
    width: 300px;
    box-shadow: 0 0 10px yellow !important;
  }

  .btn-light {
    color: #302A18;
  }

  .description-bg {
    width: 95%; /* Установите ширину описания, например, 95% */
    margin: 0 auto; /* Центрирование описания по горизонтали */
    border-radius: 5px;
    background-color: #1e2023; /* Здесь указывается отличающийся оттенок фона */
    box-shadow: 0 0 10px gray !important;
  }
  
  .nav-link {
    font-family: 'Poppins';
  }

  /* Стили из HomeStyles.js */

  /* StyledHome */
  .home-container {
    padding: 30px;
    color: #fff;
    background-color: #000;
  }

  /* Content */
  .home-content {
    margin-bottom: 30px;
  }

  /* StyledTitle */
  .home-title {
    color: #fff;
    margin-bottom: 20px;
  }

  /* StyledParagraph */
  .home-paragraph {
    color: #fff;
    margin-bottom: 10px;
  }

  /* StyledLink */
  .home-link {
    font-size: 16px;
    color: #fff;
  }

  /* Другие стили... */

  /* Стили для страницы магазина */
  .store-container {
    padding: 30px;
  }

  .login-container {
    height: 100vh;
  }
  
  .error-message {
    color: red;
    margin-bottom: 1rem;
  }
`;

export default GlobalStyles;
