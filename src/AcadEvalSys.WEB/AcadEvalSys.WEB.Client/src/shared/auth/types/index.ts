export interface User {
  id: string;
  email: string;
  name?: string;
  roles: string[];
}

export interface LoginCredentials {
  email: string;
  password: string;
}

export interface LoginResponse {
  tokenType: string;
  accessToken: string;
  expiresIn: number;
  refreshToken: string;
}

export interface ApiError {
  message: string;
  errors?: Record<string, string[]>;
}
