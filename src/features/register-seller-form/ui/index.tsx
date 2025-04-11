import { Button, FormControl, FormHelperText, IconButton, InputAdornment, InputLabel, OutlinedInput, TextField, Typography } from "@mui/material";
import { useState } from "react";
import styles from "./index.module.css";
import * as Yup from "yup";
import { Visibility, VisibilityOff } from "@mui/icons-material";
import { ProfileRegister, SellerRegister, UserRegister } from "@/shared/services/auth-service-user";
import { SellerForm } from "@/shared/forms/register-form";

interface Props {
  onRegister: (user: UserRegister, profile: ProfileRegister, seller: SellerRegister) => void;
}

export const RegisterSellerForm = ({ onRegister }: Props) => {
  const [formData, setFormData] = useState<SellerForm>({
    login: "SomeSeller",
    email: "seller@gmail.com",
    phoneNumber: "2223334455",
    password: "u2s433r7S1@$%4",
    confirmPassword: "u2s433r7S1@$%4",
    name: "Дмитрий",
    surname: "Орлов",
    patronymic: "Савельевич",
  });

  const [errors, setErrors] = useState<Record<string, string>>({});
  const [showPassword, setShowPassword] = useState(false);
  const [showConfirmPassword, setShowConfirmPassword] = useState(false);

  const validationSchema = Yup.object({
    login: Yup.string().required("Логин обязателен").min(5, "Логин должен быть больше 5 символов"),
    email: Yup.string().required("Email обязателен").email("Неверный адрес электронной почты"),
    phoneNumber: Yup.string()
      .required("Номер телефона обязателен")
      .matches(/^\d{10}$/, "Неверный номер телефона"),
    password: Yup.string()
      .required("Пароль обязателен")
      .min(8, "Пароль должен быть больше 8 символов")
      .matches(/[!@#$%^&*(),.?:|<>]/, "Пароль должен содержать как минимум один специальный символ")
      .matches(/[0-9]/, "Пароль должен содержать цифры")
      .matches(/[A-Z]/, "Пароль должен содержать латинские буквы верхнего регистра")
      .matches(/[a-z]/, "Пароль должен содержать латинские буквы нижнего регистра"),
    confirmPassword: Yup.string()
      .required("Повторите пароль")
      .oneOf([Yup.ref("password")], "Пароли не совпадают"),
    name: Yup.string().required("Имя обязательно"),
    surname: Yup.string().required("Фамилия обязательна"),
    patronymic: Yup.string().required("Отчество обязательно"),
  });

  const handleChangeFormData = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
    // Очищаем ошибку при изменении поля
    if (errors[name]) {
      setErrors((prev) => ({ ...prev, [name]: "" }));
    }
  };

  const handlePhoneNumberChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = e.target.value.replace(/\D/g, "").slice(0, 10);
    setFormData((prev) => ({ ...prev, phoneNumber: value }));
  };

  const handleSubmitForm = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await validationSchema.validate(formData, { abortEarly: false });
      setErrors({});

      const userRegister: UserRegister = {
        login: formData.login,
        email: formData.email,
        phoneNumber: `7${formData.phoneNumber}`,
        password: formData.password,
      };

      const profileRegister: ProfileRegister = {
        name: formData.name,
        surname: formData.surname,
        patronymic: formData.patronymic,
      };

      const sellerRegister: SellerRegister = {};

      onRegister(userRegister, profileRegister, sellerRegister);
    } catch (error: any) {
      const newErrors: Record<string, string> = {};
      error.inner.forEach((err: any) => {
        newErrors[err.path] = err.message;
      });
      setErrors(newErrors);
    }
  };

  const togglePasswordVisibility = (field: "password" | "confirmPassword") => {
    if (field === "password") {
      setShowPassword((prev) => !prev);
    } else {
      setShowConfirmPassword((prev) => !prev);
    }
  };

  return (
    <div className={styles.main}>
      <Typography variant="h4" className={styles.header}>
        Регистрация как продавец
      </Typography>

      <form onSubmit={handleSubmitForm}>
        <div className={styles.containerForm}>
          <TextField
            error={!!errors.login}
            helperText={errors.login}
            required
            label="Логин"
            name="login"
            value={formData.login}
            onChange={handleChangeFormData}
            fullWidth
          />

          <TextField
            error={!!errors.email}
            helperText={errors.email}
            required
            label="Email"
            name="email"
            type="email"
            value={formData.email}
            onChange={handleChangeFormData}
            fullWidth
          />

          <FormControl error={!!errors.phoneNumber} fullWidth>
            <InputLabel>Номер телефона</InputLabel>
            <OutlinedInput
              startAdornment={<InputAdornment position="start">+7</InputAdornment>}
              label="Номер телефона"
              name="phoneNumber"
              value={formData.phoneNumber}
              onChange={handlePhoneNumberChange}
              inputProps={{ maxLength: 11 }}
            />
            {errors.phoneNumber && <FormHelperText error>{errors.phoneNumber}</FormHelperText>}
          </FormControl>

          <FormControl error={!!errors.password} fullWidth variant="outlined">
            <InputLabel>Пароль</InputLabel>
            <OutlinedInput
              type={showPassword ? "text" : "password"}
              value={formData.password}
              onChange={handleChangeFormData}
              name="password"
              endAdornment={
                <InputAdornment position="end">
                  <IconButton onClick={() => togglePasswordVisibility("password")} onMouseDown={(e) => e.preventDefault()} edge="end">
                    {showPassword ? <VisibilityOff /> : <Visibility />}
                  </IconButton>
                </InputAdornment>
              }
              label="Пароль"
            />
            {errors.password && <FormHelperText error>{errors.password}</FormHelperText>}
          </FormControl>

          <FormControl error={!!errors.confirmPassword} fullWidth variant="outlined">
            <InputLabel>Подтвердите пароль</InputLabel>
            <OutlinedInput
              type={showConfirmPassword ? "text" : "password"}
              value={formData.confirmPassword}
              onChange={handleChangeFormData}
              name="confirmPassword"
              endAdornment={
                <InputAdornment position="end">
                  <IconButton onClick={() => togglePasswordVisibility("confirmPassword")} onMouseDown={(e) => e.preventDefault()} edge="end">
                    {showConfirmPassword ? <VisibilityOff /> : <Visibility />}
                  </IconButton>
                </InputAdornment>
              }
              label="Подтвердите пароль"
            />
            {errors.confirmPassword && <FormHelperText error>{errors.confirmPassword}</FormHelperText>}
          </FormControl>

          <TextField
            error={!!errors.name}
            helperText={errors.name}
            required
            label="Имя"
            name="name"
            value={formData.name}
            onChange={handleChangeFormData}
            fullWidth
          />

          <TextField
            error={!!errors.surname}
            helperText={errors.surname}
            required
            label="Фамилия"
            name="surname"
            value={formData.surname}
            onChange={handleChangeFormData}
            fullWidth
          />

          <TextField
            error={!!errors.patronymic}
            helperText={errors.patronymic}
            required
            label="Отчество"
            name="patronymic"
            value={formData.patronymic}
            onChange={handleChangeFormData}
            fullWidth
          />
          <Button type="submit" variant="contained" className={styles.buttonRegister} fullWidth>
            Зарегистрироваться
          </Button>
        </div>
      </form>
    </div>
  );
};
