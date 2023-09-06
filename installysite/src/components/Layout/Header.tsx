'use client'

import { styled } from 'styled-components';
import { useRouter } from 'next/navigation';
import Link from 'next/link';
import { useContext, useState } from 'react';

import { Button, Container } from '@/styles/layout';
import Logo from 'public/logo.svg';
import { DashboardContext } from '@/contexts/Dashboard';
import Profile from './Profile';

// Menu Dashboard SVG Elements

const Wrapper = styled.div<{ scroll: number }>`
  display: flex;
  align-items: center;

  box-shadow: ${({ scroll }) => scroll > 0 ? `0 0 80px rgba(0,0,0,.1);` : `none`};
  position: relative;
  z-index: 999;
  background-color: #FFF;

  * {
    user-select: none;
    -o-user-select: none;
    -moz-user-select: none;
    -webkit-user-select: none;
  }
`;

const Content = styled.div<{ active: number, pagetype: "Institutional" | "Dashboard" }>`
  display: grid;
  grid-template-columns: ${({ pagetype }) => pagetype === "Institutional" ? "1fr" : "1fr auto 1fr"};
  
  height: var(--header-size);
  position: relative;
  
  .logo {
    max-height: 100%;
    max-width: 250px;
    cursor: pointer;
    height: 100%;
    margin: ${({ pagetype }) => pagetype === "Institutional" ? "50px auto" : "0"};
  }

  .menu {
    margin: 0 auto;
    display: flex;
    align-items: center;

    --color: rgba(0,0,0,.8);

    a, .button {
      margin: 0 5px;
      padding: 15px;
      text-decoration: none;
      font-size: 16px;
      font-weight: 600;
      color: var(--color);

      display: flex;
      transition: 200ms ease-in-out;

      &.mobile { display: none; }

      &:hover {
        color: rgba(0,0,0,1);
      }

      svg {
        --size: 20px;
        min-width: var(--size);
        max-width: var(--size);
        max-height: var(--size);
        margin: 0 10px 0 0;
        fill: var(--color);

        path {
          fill: var(--color);
        }
      }

      span {
        margin: 2px 0 0;
        white-space: nowrap;
      }
      
      &.active {
        font-weight: 800;
        color: var(--green);
        position: relative;

        svg {
          fill: var(--green);

          path {
            fill: var(--green);
          }
        }

        &::before {
          content: "";
          width: 18px;
          height: 4px;
          border-radius: 6px;
          background: var(--green);

          position: absolute;
          bottom: 5px;
          left: 0;
          right: 0;
          margin: auto;
        }
      }
    }
  }

  .actions {
    display: flex;
    justify-content: flex-end;
    align-items: center;
  }

  .toogleMobile { display: none; }

  @media (max-width: 1280px) {
		grid-template-columns: ${({ pagetype }) => pagetype === "Institutional" ? `1fr auto auto` : `1fr auto 80px`};

    .goToMyAccount { margin: 0 0 0 20px; }
	}

  @media (max-width: 991px) {
    height: calc(var(--header-size) - 20px);

    .menu {
      background: #FFF;
      border-top: 1px solid rgba(0,0,0,.1);

      position: absolute;
      overflow: hidden;
      width: ${({ active }) => active === 1 ? 'calc(100% + 120px)' : '0'};
      right: -60px;

      top: calc(var(--header-size) - 20px);
      height: calc(100vh - var(--header-size) + 20px);
      z-index: 999;

      //transition: right 200ms ease-in-out;

      flex-direction: column;

      a, .button {
        padding: 30px 50px;
        width: 100%;
        border: none;
        background: transparent;
        border-bottom: 1px solid rgba(0,0,0,.1);

        font-size: 18px;

        &.mobile {
          display: flex;
        }

        svg {
          --size: 24px;
          margin-right: 20px;
          margin-bottom: 2px;
          margin-top: -2px;
        }

        &.active {

          &::before {
            display: none;
          }
        }

        &:hover {
          background: rgba(0,0,0,.05);
        }
      }
    }

    .goToMyAccount { margin: 0 20px; }

    .toogleMobile {
      display: flex;
      flex-direction: column;

      --size: 60px;
      width: var(--size);
      height: var(--size);
      margin: auto;
      border-radius: 8px;
      padding: 10px;
      cursor: pointer;

      margin-right: -20px;

      position: relative;

      div {
        width: 100%;
        height: 5px;
        border-radius: 10px;
        background: #333;
        margin: 4px 0;

        transition: 100ms ease-in-out, width 0s, height 0s;
        
        ${({ active }) => active && `
          &:nth-of-type(2) {
            opacity: 0;
          }

          position: absolute;
          top: 0;
          bottom: 0;
          right: 0;
          left: 0;
          margin: auto;
          width: 70%;

          transform: rotate(45deg);

          &:nth-of-type(3) {
            transform: rotate(-45deg);
          }
        `};
      }

      transition: 200ms ease-in-out;

      &:hover {
        background: rgba(0,0,0,.1);
      }
    }
  }

  @media (max-width: 680px) {
    .goToMyAccount { display: none; }
  }

  @media (max-width: 576px) {
    .logo {
      max-width: 200px;
    }

    .menu {
      width: ${({ active }) => active === 1 ? 'calc(100% + 60px)' : '0'};
      right: -30px;

      a, .button {
        padding: 25px 30px;
      }
    }

    .toogleMobile {
      margin-right: -6px;
      --size: 56px;
    }
  }

  @media (max-width: 420px) {
    .logo {
      max-width: 85%;
    }
  }
`;

export default function Header({ title, dashboard, conversations, scroll = 0 }: { title: string, dashboard?: boolean, conversations?: boolean, scroll: number }) {

  const [menuMobileState, setMenuMobileState] = useState(false);

  const { isAuthenticated, user, loggout } = useContext(DashboardContext);

  const router = useRouter();

  function logoClick()
  {
    if(dashboard && isAuthenticated) return router.push("/admin/leads");
    return router.push("/");
  }

  function getMenu()
  {
    if(dashboard && isAuthenticated) return [
      { icon: <div />, iconSelected: <div />, title: "Leads", pathname: "/admin/leads", className: "" },
      { icon: <div />, iconSelected: <div />, title: "Forms", pathname: "/admin/forms", className: "" },
    ];

    return [
      // { icon: null, title: "Início", pathname: "/", className: "" },
    ];
  }

  function getActions()
  {
    if(!dashboard) return <Button onClick={() => router.push("/admin/admin")} className="goToMyAccount">Minha conta</Button>
    else {
      if(isAuthenticated) return <Profile user={user} loggout={loggout} closeMenuMobile={() => setMenuMobileState(false)} />
      //else return <Button className="gray" onClick={() => router.push("/admin")}><HelpIcon /> Preciso de ajuda</Button>
    }
  }

  return <Wrapper id="header" scroll={scroll}>
    <Container>
      <Content active={menuMobileState ? 1 : 0} pagetype={(dashboard && isAuthenticated) ? "Dashboard" : "Institutional"}>
        <Logo className="logo" onClick={() => logoClick()} />
        
        {
          isAuthenticated ? <>
            <div className="menu">
            { getMenu()?.map((item: any, i: number) => 
              item.pathname ? (
                <Link key={i} href={item.pathname} className={`${item?.className} ${item.title === title ? `active` : ""}`}>
                  {item?.icon ? (item.title === title ? item?.iconSelected : item.icon) : ""}
                  <span>{item.title}</span>
                </Link>
              ) : (
                <div key={i} onClick={item.onClick} className={`button ${item?.className} ${item.title === title ? `active` : ""}`}>
                  {item?.icon ? (item.title === title ? item?.iconSelected : item.icon) : ""}
                  <span>{item.title}</span>
                </div>
              )
            )}
          </div>

          <div className="actions">{getActions()}</div>

          <div className="toogleMobile" onClick={() => setMenuMobileState(!menuMobileState)}>
            <div></div>
            <div></div>
            <div></div>
          </div>
          </> : <></>
        }
      </Content>
    </Container>
  </Wrapper>
}