import { Button, FormControl, FormHelperText, IconButton, InputAdornment, InputLabel, OutlinedInput, TextField, Typography } from "@mui/material";
import { useCallback, useState } from "react";
import * as Yup from "yup";
import styles from "./index.module.css";
import { Visibility, VisibilityOff } from "@mui/icons-material";
import { UserLogin } from "../../../shared/services/auth-service-user";
import { LoginForm } from "../../../shared/forms/login-form";

type Props = {
  onLogin: (user: UserLogin) => void;
};

type LoginVariant = "email" | "phoneNumber" | "login";

export const LoginUserForm = ({ onLogin }: Props) => {
  const [formData, setFormData] = useState<LoginForm>({
    login: "",
    email: null,
    phoneNumber: null,
    password: "u2s433r7S1@$%4",
  });

  const [activeVariant, setActiveVariant] = useState<LoginVariant>("login");
  const [secondaryVariants, setSecondaryVariants] = useState<[LoginVariant, LoginVariant]>(["phoneNumber", "email"]);

  const [errors, setErrors] = useState<Record<string, string>>({});
  const [showPassword, setShowPassword] = useState(false);

  // Динамическая схема валидации в зависимости от активного варианта
  const getValidationSchema = useCallback(() => {
    const baseSchema = {
      password: Yup.string()
        .required("Пароль обязателен")
        .min(8, "Пароль должен быть больше 8 символов")
        .matches(/[!@#$%^&*(),.?:|<>]/, "Пароль должен содержать как минимум один специальный символ")
        .matches(/[0-9]/, "Пароль должен содержать цифры")
        .matches(/[A-Z]/, "Пароль должен содержать латинские буквы верхнего регистра")
        .matches(/[a-z]/, "Пароль должен содержать латинские буквы нижнего регистра"),
    };

    const loginSchema = {
      login: Yup.string().required("Логин обязателен").min(5, "Логин должен быть больше 5 символов"),
    };

    const emailSchema = {
      email: Yup.string().required("Email обязателен").email("Неверный адрес электронной почты"),
    };

    const phoneSchema = {
      phoneNumber: Yup.string()
        .required("Номер телефона обязателен")
        .matches(/^\d{10}$/, "Неверный номер телефона"),
    };

    // Добавляем только необходимую схему в зависимости от активного варианта
    let schemaFields = { ...baseSchema };
    if (activeVariant === "login") schemaFields = { ...schemaFields, ...loginSchema };
    if (activeVariant === "email") schemaFields = { ...schemaFields, ...emailSchema };
    if (activeVariant === "phoneNumber") schemaFields = { ...schemaFields, ...phoneSchema };

    return Yup.object(schemaFields);
  }, [activeVariant]);

  // Обработчик изменения полей формы
  const handleChange = useCallback(
    (e: React.ChangeEvent<HTMLInputElement>) => {
      const { name, value } = e.target;
      setFormData((prev) => ({ ...prev, [name]: value }));

      // Очищаем ошибку при изменении значения
      if (errors[name]) setErrors((prev) => ({ ...prev, [name]: "" }));
    },
    [errors]
  );

  // Специальный обработчик для номера телефона
  const handlePhoneChange = useCallback(
    (e: React.ChangeEvent<HTMLInputElement>) => {
      const value = e.target.value.replace(/\D/g, "").slice(0, 10);
      setFormData((prev) => ({ ...prev, phoneNumber: value }));

      if (errors.phoneNumber) setErrors((prev) => ({ ...prev, phoneNumber: "" }));
    },
    [errors.phoneNumber]
  );

  // Переключение вариантов входа с очисткой неактивных полей
  const switchVariant = useCallback((variant: LoginVariant) => {
    // Сбрасываем значения неактивных полей
    setFormData((prev) => {
      const newFormData = { ...prev };
      // Сбрасываем все поля кроме пароля
      if (variant !== "login") newFormData.login = null;
      if (variant !== "email") newFormData.email = null;
      if (variant !== "phoneNumber") newFormData.phoneNumber = null;
      return newFormData;
    });

    // Сбрасываем ошибки
    setErrors({});

    setActiveVariant(variant);

    // Обновляем доступные варианты для переключения
    if (variant === "login") {
      setSecondaryVariants(["email", "phoneNumber"]);
    } else {
      setSecondaryVariants([variant === "email" ? "phoneNumber" : "email", "login"]);
    }
  }, []);

  const togglePasswordVisibility = () => {
    setShowPassword((prev) => !prev);
  };

  const handleSubmitForm = async (e: React.FormEvent) => {
    e.preventDefault();

    try {
      // Подготавливаем данные для валидации
      const dataToValidate = {
        password: formData.password,
        [activeVariant]: formData[activeVariant],
      };

      // Используем только нужную схему валидации
      await getValidationSchema().validate(dataToValidate, {
        abortEarly: false,
      });

      // Очищаем ошибки после успешной валидации
      setErrors({});

      // Подготавливаем данные для отправки
      const userLogin: UserLogin = {
        login: activeVariant === "login" ? formData.login : null,
        email: activeVariant === "email" ? formData.email : null,
        phoneNumber: activeVariant === "phoneNumber" ? `7${formData.phoneNumber}` : null,
        password: formData.password,
      };

      // Вызываем функцию авторизации
      onLogin(userLogin);
    } catch (error: any) {
      if (error.inner) {
        const newErrors: Record<string, string> = {};
        error.inner.forEach((err: any) => {
          newErrors[err.path] = err.message;
        });
        setErrors(newErrors);
      } else {
        console.error("Ошибка валидации:", error);
      }
    }
  };

  // Рендер активного поля ввода
  const renderActiveField = () => {
    const commonProps = {
      error: !!errors[activeVariant],
      helperText: errors[activeVariant] || " ",
      fullWidth: true,
      sx: { mb: 2 },
    };

    switch (activeVariant) {
      case "login":
        return <TextField {...commonProps} label="Логин" name="login" value={formData.login || ""} onChange={handleChange} />;

      case "email":
        return <TextField {...commonProps} label="Email" type="email" name="email" value={formData.email || ""} onChange={handleChange} />;

      case "phoneNumber":
        return (
          <FormControl error={!!errors.phoneNumber} fullWidth sx={{ mb: 2 }}>
            <InputLabel>Номер телефона</InputLabel>
            <OutlinedInput
              value={formData.phoneNumber || ""}
              onChange={handlePhoneChange}
              name="phoneNumber"
              startAdornment={<InputAdornment position="start">+7</InputAdornment>}
              inputProps={{ maxLength: 10 }}
              label="Номер телефона"
            />
            <FormHelperText>{errors.phoneNumber || " "}</FormHelperText>
          </FormControl>
        );
    }
  };

  return (
    <div className={styles.main}>
      <Typography variant="h4" className={styles.header}>
        Авторизация
      </Typography>

      <form onSubmit={handleSubmitForm}>
        <div className={styles.containerForm}>
          {renderActiveField()}

          <FormControl error={!!errors.password} fullWidth variant="outlined">
            <InputLabel>Пароль</InputLabel>
            <OutlinedInput
              type={showPassword ? "text" : "password"}
              value={formData.password}
              onChange={handleChange}
              name="password"
              endAdornment={
                <InputAdornment position="end">
                  <IconButton onClick={togglePasswordVisibility} onMouseDown={(e) => e.preventDefault()} edge="end">
                    {showPassword ? <VisibilityOff /> : <Visibility />}
                  </IconButton>
                </InputAdornment>
              }
              label="Пароль"
            />
            <FormHelperText>{errors.password || " "}</FormHelperText>
          </FormControl>

          <Button variant="outlined" onClick={() => switchVariant(secondaryVariants[0])}>
            Войти по {secondaryVariants[0] === "phoneNumber" ? "номеру телефона" : secondaryVariants[0]}
          </Button>

          <Button variant="outlined" onClick={() => switchVariant(secondaryVariants[1])}>
            Войти по {secondaryVariants[1] === "phoneNumber" ? "номеру телефона" : secondaryVariants[1]}
          </Button>

          <Button type="submit" variant="contained" className={styles.buttonRegister} fullWidth>
            Войти
          </Button>
        </div>
      </form>
    </div>
  );
};
