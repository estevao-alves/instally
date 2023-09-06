import { styled } from "styled-components";
import { formatFullName, formatDocument } from "@/helpers/format";

import UserSvg from "@/assets/icons/user.svg";
import LoggoutSvg from "@/assets/icons/loggout.svg";
import { useState } from "react";
import { getUserInitials } from "@/helpers/user";
import { useRouter } from "next/navigation";

const ProfileWrapper = styled.div<{ active: number }>`
	position: relative;
	margin-right: -20px;
	
	.content {
		z-index: 2;
		position: relative;
		display: flex;
		align-items: center;
		text-align: right;
		padding: 15px;
		cursor: pointer;
		transition: 200ms ease-in-out;
		border-radius: 6px;

		&:hover {
			background: rgba(0,0,0,.1);
		}

		.info {
			margin: 0 10px;

			h4 {
				font-size: 18px;
				font-weight: 600;
			}

			span {
				color: rgba(0, 0, 0, 0.60);
				font-weight: 600;
			}
		}

		.image {
			min-width: 55px;
			max-width: 55px;
			height: 55px;
			border-radius: 100%;
			background-color: var(--green);
			background-size: cover;
			background-repeat: no-repeat;
			background-position: center;

			display: flex;
			justify-content: center;
			align-items: center;

			.initials {
				font-size: 20px;
				font-weight: 600;
				color: white;
				margin: 2px 2px 0 0;
			}
		}
	}

	@media (max-width: 1280px) {
		.content {
			.info {
				display: none;
			}
		}
	}

	@media (max-width: 991px) {
		.content {
			padding: 0;
			border-radius: 100%;
		}
	}

	@media (max-width: 576px) {
		margin-right: -20px;

		.content {

			&:after {
				content: "";
				width: 100%;
				height: ${({ active }) => active === 1 ? "100vh" : "0"};
				position: fixed;
				top: 0;
				left: 0;
				z-index: 1;
				background: rgba(118,118,118,.3);
				backdrop-filter: blur(2px);
			}
		}
  }
`;

const MenuOptions = styled.div<{ active: number }>`
	position: absolute;
	top: ${({ active }) => active === 1 ? "100px" : "-200px"};
	opacity: ${({ active }) => active === 1 ? "1" : "0"};
	right: 0;

	background: #FFF;
	border-radius: 6px;
	width: 260px;
	overflow: hidden;

	transition: ${({ active }) => active === 1 ? `200ms ease-in-out, opacity .2s .1s ease-in-out` : `80ms ease-in-out, opacity 40ms ease-in-out`};
	z-index: 1;

	box-shadow: 0 0 40px rgba(0,0,0,.1);
	-o-box-shadow: 0 0 40px rgba(0,0,0,.1);
	-moz-box-shadow: 0 0 40px rgba(0,0,0,.1);
	-webkit-box-shadow: 0 0 40px rgba(0,0,0,.1);

	.option {
		opacity: ${({ active }) => active === 1 ? "1" : "0"};
		padding: 20px;
		display: flex;
		justify-content: flex-start;
		transition: ${({ active }) => active === 1 ? '200ms ease-in-out, opacity 200ms ease-in-out .2s' : '0' };

		display: flex;
		align-items: center;
		cursor: pointer;
		
		svg {
			--size: 20px;
			width: var(--size);
			height: var(--size);
			margin: 0 15px 0 5px;
		}

		span {
			font-size: 18px;
			font-weight: 600;
		}

		&:hover {
			svg {
				fill: var(--green);

				path {
					fill: var(--green);
				}
			}

			span {
				color: var(--green);
			}
		}
	}

	.separator {
		height: 1px;
		background: rgba(0,0,0,.1)
	}

	@media (max-width: 991px) {
		top: ${({ active }) => active === 1 ? "80px" : "-200px"};
	}

	@media (max-width: 576px) {
		position: fixed;
    bottom: 0;
    top: initial;
    width: 100%;
    height: ${({ active }) => active === 1 ? "40vh" : "0"};
    z-index: 999;
	}

`;

const Profile = ({ user, loggout, closeMenuMobile }: any) => {

	//user.image = "https://github.com/gabrielafonsodev.png";

	const [menuOpened, setMenuOpened] = useState(false);

	const router = useRouter();

	return <ProfileWrapper active={menuOpened ? 1 : 0} onClick={() => {
		closeMenuMobile();
		setMenuOpened(!menuOpened);
	}}>
		<div className="content">
			<div className="info">
				<h4>{formatFullName(user?.name)}</h4>
				<span>CPF: {formatDocument(user?.document, { hideLastCharacters: true })}</span>
			</div>
			<div className="image" style={{ backgroundImage: `url(${user?.image})` }}>
				<span className="initials">{getUserInitials(user?.name)}</span>
			</div>
		</div>

		<MenuOptions active={menuOpened ? 1 : 0}>
			<div className="option" onClick={() => router.push("/admin/perfil")}>
				<UserSvg />
				<span>Meu perfil</span>
			</div>

			<div className="separator" />

			<div className="option" onClick={() => loggout()}>
				<LoggoutSvg />
				<span>Desconectar</span>
			</div>
		</MenuOptions>
	</ProfileWrapper>
}

export default Profile;