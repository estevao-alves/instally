import { Context, createContext, useEffect, useState } from "react";
import { parseCookies } from 'nookies';

import Loading from "@/components/Loading";
import { loggout, login, recoveryUserData } from "@/services/auth";
import { useRouter } from "next/navigation";

export type UserTypes = {
	_id?: string;
	name?: string;
	document?: string;
	email?: string;
	phone?: string;
}

export type ConversationsTypes = {
	visible: boolean;
	loading?: {
		text: string;
	} | null;
}

type ContextTypes = {
	isAuthenticated: boolean;
	user: UserTypes | null;
	login: (data: UserTypes) => void;
	loggout: () => void;

	loading: boolean;
	setLoading: (state: boolean) => void;

	scroll: number;
	setScroll: (value: number) => void; 
}

export const DashboardContext: Context<ContextTypes> = createContext({} as ContextTypes);

export const DashboardProvider = ({ children }: any) => {

  const [scroll, setScroll] = useState(0);
	const [prepared, setPrepared] = useState(false);
	const [user, setUser] = useState<UserTypes | null>(null);
	
	const { ["token"]: refreshToken } = parseCookies(null);
	
	const router = useRouter();

	useEffect(() => {
		const userRecovered = recoveryUserData(refreshToken);
		setUser(userRecovered);

		if(!userRecovered && window.location.pathname !== "/admin") router.push("/admin");
		if(userRecovered && window.location.pathname === "/admin") router.push("/admin/leads");
	}, [refreshToken]);

	useEffect(() => {
		document.getElementById("header")?.scrollIntoView({ behavior: "smooth", block: "start" });
		if(!!user && window.location.pathname === "/admin") setPrepared(false);
	}, [user]);

	const contextFunctions: ContextTypes = {
		isAuthenticated: !!user,
		user,
		login: (userData: UserTypes) => setUser(login(userData)),
		loggout: () => setUser(loggout()),

		loading: !prepared,
		setLoading: (state: boolean) => setPrepared(!state),

		scroll,
		setScroll
	}

	return <DashboardContext.Provider value={contextFunctions}>
		{ !prepared ? <Loading /> : <></> }
		{children}
	</DashboardContext.Provider>
}