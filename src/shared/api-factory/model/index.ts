import axios, { AxiosInstance } from "axios";
import AuthStore from "../../../entities/stores/auth-store/model";
import { ServiceApiConfig } from "../../api/axios-services";

export const createApi = (config: ServiceApiConfig): AxiosInstance => {
  return axios.create(config);
};

export const configureApiInterceptors = (
  api: AxiosInstance,
  authStore: AuthStore
) => {
  // Request interceptor
  api.interceptors.request.use((config) => {
    if (authStore.accessToken) {
      config.headers.Authorization = `Bearer ${authStore.accessToken}`;
    }
    return config;
  });

  // Response interceptor
  api.interceptors.response.use(
    (response) => response,
    async (error) => {
      const originalRequest = error.config;

      if (error.response?.status === 401 && !originalRequest._retry) {
        originalRequest._retry = true;

        await authStore.refreshTokenAction();

        originalRequest.headers.Authorization = `Bearer ${authStore.accessToken}`;
        return api(originalRequest);
      }

      return Promise.reject(error);
    }
  );
};
