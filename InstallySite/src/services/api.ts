import axios from "axios";
import configs from "../../configs";

export const api = getApiClient();

function getApiClient(ctx?: any) {
  
  const api = axios.create({
    baseURL: `${configs.domain}/api`
  })
  
  return api;
}