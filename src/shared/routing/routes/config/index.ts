export const ROUTES = {
  MAIN: {
    path:"/",
    base: "/"
  },
  PRODUCT: {
    path: (title: string, id: string) => 
      `/product/${title}?id=${id}`,

    base: "/product/:title",
  },
  PROFILE: {
    path: (id: string) =>
        `/profile/?id=${id}`,
    base: "/profile"
  },
  ADD_PRODUCT: {
    path: () =>
        '/product/add',
    base: "/product/add"
  },
  SETTINGS: {
    path: () =>
        '/profile/settings',
    base: "/profile/settings"
  }
  
} as const;