import jwt from "jsonwebtoken";
import { destroyCookie, setCookie } from "nookies";

import { api } from "./api";
import { UserTypes } from "@/contexts/Dashboard";

class Auth {
  token;
  user: UserTypes;

  constructor(request: Request) {
    let bearerAuth = request.headers.get("Authorization");
    this.token = bearerAuth?.split("Bearer ")[1];
    this.user = this.decodeToken();
  }

  decodeToken() {
    if(!this.token) return null;
    return JSON.parse(JSON.stringify(jwt.decode(this.token)));
  }

  isValid() {
    return !!this.token && !!this.user && !!this.user._id;
  }
}

function login(userData: UserTypes): UserTypes | null
{
  if(!userData) return null;

  const token = jwt.sign(userData, "MYSECRETKEY");

  setCookie(undefined, 'token', token, {
      maxAge: 60 * 60 * 24 * 365,
      path: "/"
  });

  api.defaults.headers['Authorization'] = `Bearer ${token}`;

  return userData;
}

function loggout(): null
{
  destroyCookie(undefined, 'token', { path: "/" });
  api.defaults.headers['Authorization'] = "";
  return null;
}

function recoveryUserData(token: string)
{
  if(!token) return null;

  const { _id, name, document, email, phone } = jwt.decode(token) as UserTypes;
  if(!_id || !document) return null;

  api.defaults.headers['Authorization'] = `Bearer ${token}`;
  return { _id, name, document, email, phone };
}

export { Auth, login, loggout, recoveryUserData };