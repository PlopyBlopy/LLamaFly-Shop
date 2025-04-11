export enum UserRoleType {
  Admin = "admin",
  Seller = "seller",
  Customer = "customer",
  UnAuthorize = "unAuthorize",
}

export type UserPayload = {
  sub: string;
  role: UserRoleType;
};

export type User = {
  id: string;
  role: UserRoleType;
};

// export type Admin = {
//   name: string;
//   surname: string;
//   patronymic: string;
// };

// export type Seller = {
//   name: string;
//   surname: string;
//   patronymic: string;
// };

// export type Customer = {
//   name: string;
//   surname: string;
//   patronymic: string;
// };

export type AdminResponse = {
  name: string;
  surname: string;
  patronymic: string;
};

export type SellerResponse = {
  name: string;
  surname: string;
  patronymic: string;
};

export type CustomerResponse = {
  name: string;
  surname: string;
  patronymic: string;
};

export type Admin = User & {
  name: string;
  surname: string;
  patronymic: string;
};

export type Seller = User & {
  name: string;
  surname: string;
  patronymic: string;
};

export type Customer = User & {
  name: string;
  surname: string;
  patronymic: string;
};
