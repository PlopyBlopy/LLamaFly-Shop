export type UserLogin = {
  login?: string | null;
  email?: string | null;
  phoneNumber?: string | null;
  password: string;
};

export type UserRegister = {
  login: string;
  email: string;
  phoneNumber: string;
  password: string;
};

export type ProfileRegister = {
  name: string;
  surname: string;
  patronymic: string;
};

export type SellerRegister = {};

export type CustomerRegister = {};

export type UserProfileSellerRegister = {
  user: UserRegister;
  profile: ProfileRegister;
  seller: SellerRegister;
};

export type UserProfileCustomerRegister = {
  user: UserRegister;
  profile: ProfileRegister;
  customer: CustomerRegister;
};
