export type UserForm = {
  login: string;
  email: string;
  phoneNumber: string;
  password: string;
  confirmPassword: string;
};

export type SellerForm = UserForm & {
  name: string;
  surname: string;
  patronymic: string;
};

export type CustomerForm = UserForm & {
  name: string;
  surname: string;
  patronymic: string;
};
