import { styled } from "styled-components";

export const Wrapper = styled.div`
  min-height: 100vh;
  padding: 60px 0;
  overflow: hidden;
  background-color: var(--background-color);

  @media (max-width: 576px) {
    padding: 40px 0;
  }
`;

export const Content = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;

  max-width: 800px;
  margin: 0 auto;
  
  .logo {
    width: 100%;
    max-width: 420px;
    margin: 60px 0;
  }

  .line {
    margin: 30px 0;
    height: 1px;
    background: rgba(0,0,0,.15);
    width: 100%;
  }

  .text {
    text-align: center;
    max-width: 740px;

    h1 {
      font-size: 28px;
    }

    h2 {
      font-size: 20px;
      color: var(--green);
      margin: 30px 0 30px;
    }
  }

  .info {
    display: flex;
    align-items: center;
    
    border: 1px solid rgba(0,0,0,.15);
    border-radius: 10px;
    padding: 25px 30px;
    margin: 30px 0;
    
    .attention {
      text-align: left;
      margin-right: 30px;
      font-size: 13px;
      font-weight: 600;
      opacity: .8;
    }

    .timer {
      display: flex;
      flex-direction: column;
      align-items: center;

      padding: 15px 20px 10px;
      border-radius: 10px;

      background: #BDE3D0;
      width: max-content;
      
      span {
        --color: #0E472A;
        color: var(--color);
        text-transform: uppercase;
        font-weight: 700;
        font-size: 10px;
        line-height: 1.2;
        white-space: nowrap;

        &:last-child {
          font-weight: 800;
          font-size: 28px;
        }
      }
    }
  }

  @media (max-width: 991px) {
    .logo {
      max-width: 200px;
      margin: 0 0 40px;
    }

    .text {
      max-width: 500px;

      h1 {
        font-size: 20px;
      }

      h2 {
        font-size: 16px;
        margin: 30px 0 30px;
      }
    }

    .info {
      flex-direction: column;
      padding: 25px 30px;
      
      .attention {
        margin: 0 0 20px;
        text-align: center;
        max-width: 500px;
      }
    }
  }

  @media (max-width: 620px) {
    .logo {
      margin: 0 auto 40px 0;
    }

    .text {
      max-width: 500px;
      text-align: left;

      h1 {
        font-size: 26px;
      }

    }

    .info {
      padding: 20px;
      margin: -5px -10px;
      
      .attention {
        margin: 0 0 20px;
        text-align: left;
        max-width: 500px;
      }
    }
  }

`;

export const Fields = styled.div`
  width: 100%;
  max-width: 560px;
  margin: 40px auto;

  .item {
    margin: 0 0 40px;
    
    .important {
      font-size: 14px;
      font-weight: 700;
      margin: 6px 0;
      color: var(--green);
    }

    &.error {
      border: 1px solid #cf1b1b9e;
      border-radius: 10px;
      padding: 20px 20px 10px;
      margin: 20px -20px;
      background: #ff000005;
    }
  }

  .errorMessage {
    font-size: 14px;
    font-weight: 600;
    color: #cf1b1b9e;
  }

  .double {
    display: grid;
    grid-template-columns: 1fr 1fr;
    gap: 10px;
    margin: 10px 0 0;
  }

  Button {
    width: 100%;
  }

`;

export const UploadItems = styled.div`
  
  .item-image {
    display: flex;
    align-items: center;

    background: rgba(0,0,0,.1);
    padding: 5px;
    border-radius: 10px;
    margin: 10px 0 0;

    --size: 50px;

    img {
      width: var(--size);
      min-width: var(--size);
      height: var(--size);
      border-radius: 6px;
      object-fit: cover;
    }

    span {
      width: 100%;
      padding: 0 20px;
      font-size: 14px;
      text-overflow: ellipsis;
      max-width: 100%;
      overflow: hidden;
    }

    svg.remove {
      max-width: 20px;
      width: 30px;
      width: var(--size);
      min-width: var(--size);
      height: var(--size);
      padding: 15px;
      border-radius: var(--size);;
      background: transparent;
      transition: 200ms ease-in-out;
      cursor: pointer;

      &:hover {
        background: rgba(0,0,0,.05);
      }
    }
  }
`;