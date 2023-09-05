import styled from "styled-components";

export const InputSimplesContainer = styled.div`
  display: flex;
  flex-direction: column;
  width: 100%;
  position: relative;
  text-align: left;

  > svg {
    position: absolute;
    height: 20px;
    width: 20px;
    top: 21px;
    left: 20px;
    
    z-index: 1;
    opacity: .4;
  }

  @media (max-width: 576px) {
    > svg {
      height: 20px;
      width: 20px;
      top: 21px;
      left: 18px;
    }
  }
`;

export const InputContent = styled.div<{ icon: any, error: any }>`
  position: relative;
  margin: 0 0 10px;
  border-radius: 6px;

  color: #333;
  background: rgb(227 227 226);

  border: 2px solid ${props => props.error ? 'rgb(219, 127, 127)' : 'transparent' };
  transition: .4s ease-in-out;

  > * {
    transition: .4s ease-in-out;
  }

  input {
    cursor: pointer;
    width: 100%;
    max-width: 100%;
    border: 0;
    color: #333;
    background: transparent;
    font-size: 18px;
    font-weight: 500;
    position: relative;
    padding: ${props => props.icon === 'noIcon' ? '30px 6px 8px 16px' : '26px 6px 6px 56px'};
    line-height: 1;
    
    &::placeholder {
      color: transparent;
      -webkit-touch-callout: none; /* iOS Safari */
      -webkit-user-select: none; /* Safari */
      -khtml-user-select: none; /* Konqueror HTML */
      -moz-user-select: none; /* Old versions of Firefox */
      -ms-user-select: none; /* Internet Explorer/Edge */
      user-select: none;
    }

    &:placeholder-shown ~ ._label {
      cursor: text;
      top: 20px;
      font-size: 18px;
      font-weight: 600;
      -webkit-touch-callout: none; /* iOS Safari */
      -webkit-user-select: none; /* Safari */
      -khtml-user-select: none; /* Konqueror HTML */
      -moz-user-select: none; /* Old versions of Firefox */
      -ms-user-select: none; /* Internet Explorer/Edge */
      user-select: none;
      transition: .4s ease-in-out;
    }
    
    &:focus ~ ._label {
      position: absolute;
      transition: 0.2s;
      top: 8px;
      font-size: 16px;
      font-weight: 600;
    }

    :required, .simple__field:invalid {
      box-shadow: none;
    }

    &:disabled {
      opacity: 0.5;
      cursor: not-allowed;
      -webkit-touch-callout: none; /* iOS Safari */
      -webkit-user-select: none; /* Safari */
      -khtml-user-select: none; /* Konqueror HTML */
      -moz-user-select: none; /* Old versions of Firefox */
      -ms-user-select: none; /* Internet Explorer/Edge */
      user-select: none;
    }
  }

  &.password {
    input {
      padding-right: 60px;
    }

    .view-icon {
      width: 24px;
      height: 24px;
      position: absolute;
      right: 14px;
      top: calc(50% - 10px);
      opacity: .6;

      transition: .2s ease-in-out;
      z-index: 2;
      cursor: pointer;

      :hover {
        opacity: .8;
      }
    }
  }

  ._label {
    position: absolute;
    display: block;
    transition: 0.2s;
    font-size: 16px;
    top: 8px;
    left: ${props => props.icon === 'noIcon' ? '16px' : '56px'};
    font-weight: 600;
    color: rgba(0,0,0,.45);
    cursor: text;
    -webkit-touch-callout: none; /* iOS Safari */
    -webkit-user-select: none; /* Safari */
    -khtml-user-select: none; /* Konqueror HTML */
    -moz-user-select: none; /* Old versions of Firefox */
    -ms-user-select: none; /* Internet Explorer/Edge */
  }

  @media (max-width: 576px) {
    input {
      padding: ${props => props.icon === 'noIcon' ? '26px 6px 6px 16px' : '26px 6px 6px 50px'};
    }
    
    ._label {
      left: ${props => props.icon === 'noIcon' ? '16px' : '50px'};
    }
  }

  .textarea {
    min-height: 120px;
    max-height: 300px;
    overflow: auto;
    z-index: 98;
    padding: 34px 16px 10px;
    font-size: 18px;
    font-weight: 500;

    ~ ._label {
      top: 8px!important;
      width: calc(100% - 24px);
      padding: 10px 14px 1px;
      margin: -8px -14px 0!important;
      z-index: 99;
      background-color: rgb(227 227 226);
    }

    ~ ._label.noText {
      top: 17px!important;
      font-size: 18px;
      z-index: 97;
    }

    @media (min-width: 577px) {
      &::-webkit-scrollbar {
        width: 10px;
      }
      &::-webkit-scrollbar-thumb {
        border-width: 3px;
      }
    }
  }
`;

type ErrorTypes = {
  state?: boolean;
}

export const ErrorField = styled.div<ErrorTypes>`
  height: ${props => props.state ? 'auto' : '0'};
  padding: ${props => props.state ? '0 10px 10px' : '0'};
  margin: ${props => props.state ? '-6px 0 0' : '0'};
  border-radius: 6px;
  font-weight: 700;
  -webkit-transition: all .4s;
  color: #db7f7f;
  transition: all .4s;
  font-size: 14px;
  z-index: 2;
  
  display: flex;
  justify-content: flex-end;
`;

export const LimitCharacters = styled.div`
  padding: 10px;
  font-size: 12px;
  font-weight: 600;
  opacity: .6;

  position: absolute;
  right: 10px;
  top: 3px;
  z-index: 99;
`;