import { createGlobalStyle } from "styled-components";

const GlobalStyles = createGlobalStyle`

  :root {
    --purple-violet: #6247AA;
    --purple-simple: #7251B5;
    
    --gray: #DFDFDF;
    --white: #FFF;
    --dark-gray: #1A1A1A;
    --black: #000;
    --red: #f00;
    --header-size: 100px;

    --background-color: rgb(42 164 103 / 5%);

    --p-colorPrimaryAlpha20: hsla(150, 59%, 40%, 25%);
    --p-colorPrimaryAlpha40: hsla(150, 59%, 40%, 40%);
    --p-colorPrimaryAlpha50: hsla(150, 59%, 40%, 50%);
    --focusBoxShadow: 0 0 0 3px var(--p-colorPrimaryAlpha20), 0 0 0 1px var(--p-colorPrimaryAlpha50);
    --fontSizeSm: 0.93rem;

    touch-action: pan-x pan-y;
    height: 100%;
  }

  ::-moz-selection {
    color: var(--white);
    background: var(--purple-violet);
  }

  ::selection {
    color: var(--white);
    background: var(--purple-violet);
  }

  html {
    background: var(--dark-gray);
    color: var(--white);
  }

  /* html, body {
    overflow-x: hidden;
    max-width: 100vw;
  } */

  * {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
    outline: none;
  }

  button {
    border: 0;
  }

  body, button, input, textarea {
    font-family: var(--font-poppins), sans-serif;
  }

  @media (min-width: 577px) {
    ::-webkit-scrollbar {
      width: 14px;
      background: var(--dark-gray);
      padding: 2px;
    }
    
    /* Track */
    ::-webkit-scrollbar-track {
      border-radius: 12px;
    }
    
    /* Handle */
    ::-webkit-scrollbar-thumb {
      background-clip: padding-box;
      border-radius: 8px;
      box-shadow: none;
      min-height: 50px;
      border: 4px solid transparent;
      background-color: rgba(0,0,0,.3);
      
      :hover {
        background-clip: padding-box;
        border-radius: 8px;
        box-shadow: none;
        min-height: 50px;

        border: 3px solid transparent;
        background-color: rgba(0,0,0,.5);
      }
    
    }
  }

`;

export default GlobalStyles;