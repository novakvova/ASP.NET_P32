import { useState, useEffect } from 'react';
import {Formik, Form, Field, ErrorMessage, type FormikHelpers} from 'formik';
import * as yup from 'yup';
import { useNavigate, useSearchParams } from 'react-router-dom';
import { Loader2, Lock } from 'lucide-react';
import type {IResetPassword} from "../../Interfaces/User/IResetPassword.tsx";
import APP_ENV from "../../env";
import axios from "axios";

const validationSchema = yup.object({
    newPassword: yup.string().required("Введіть новий пароль").min(6, "Мінімум 6 символів"),
    confirmPassword: yup.string()
        .oneOf([yup.ref('newPassword')], 'Паролі не співпадають')
        .required('Підтвердіть пароль'),
});

const ResetPasswordPage = () => {
    const [searchParams] = useSearchParams();
    const navigate = useNavigate();
    const [serverError, setServerError] = useState<string | null>(null);
    const token = searchParams.get('token');
    const email = searchParams.get('email');

    useEffect(() => {
        if (!token || !email) {
            setServerError("Невірне посилання для відновлення.");
        }
    }, [token, email]);

    const handleSubmit = async (values: {newPassword: string, confirmPassword: string}, { setSubmitting }: FormikHelpers<any>) => {
        if (!token || !email) return;

        const model: IResetPassword = {
            email: email,
            token: token,
            newPassword: values.newPassword,
            confirmPassword: values.confirmPassword
        };

        try {
            await axios.post(`${APP_ENV.API_BASE_URL}/api/account/resetPassword`, model);
            alert("Пароль успішно змінено!");
            navigate("/user/LogIn");
        } catch (error: any) {
            setServerError(error.response?.data?.Errors?.Email || "Помилка при зміні пароля");
        } finally {
            setSubmitting(false);
        }
    };

    if (!token || !email) {
        return (
            <div className="min-h-screen flex justify-center items-center bg-gray-100 p-4">
                <div className="bg-white p-6 rounded-lg shadow text-center text-red-600">
                    Посилання недійсне.
                </div>
            </div>
        );
    }

    return (
        <div className="flex justify-center items-center min-h-screen bg-gray-100 p-4">
            <div className="max-w-md w-full bg-white rounded-xl shadow-lg p-8">
                <div className="text-center mb-6">
                    <div className="mx-auto w-12 h-12 bg-blue-100 rounded-full flex items-center justify-center mb-4">
                        <Lock className="text-blue-600" size={24} />
                    </div>
                    <h2 className="text-2xl font-bold text-gray-900">Новий пароль</h2>
                </div>

                {serverError && (
                    <div className="bg-red-50 text-red-600 p-3 rounded-md text-sm mb-4 text-center">
                        {serverError}
                    </div>
                )}

                <Formik
                    initialValues={{ newPassword: '', confirmPassword: '' }}
                    validationSchema={validationSchema}
                    onSubmit={handleSubmit}
                >
                    {({ isSubmitting }) => (
                        <Form className="space-y-4">
                            <div>
                                <label className="block text-sm font-medium text-gray-700 mb-1">Новий пароль</label>
                                <Field
                                    name="newPassword"
                                    type="password"
                                    className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 outline-none"
                                />
                                <ErrorMessage name="newPassword" component="div" className="text-red-500 text-xs mt-1" />
                            </div>

                            <div>
                                <label className="block text-sm font-medium text-gray-700 mb-1">Підтвердження паролю</label>
                                <Field
                                    name="confirmPassword"
                                    type="password"
                                    className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 outline-none"
                                />
                                <ErrorMessage name="confirmPassword" component="div" className="text-red-500 text-xs mt-1" />
                            </div>

                            <button
                                type="submit"
                                disabled={isSubmitting}
                                className="w-full bg-blue-600 hover:bg-blue-700 text-white font-medium py-2.5 rounded-lg transition-colors flex justify-center items-center disabled:bg-blue-400"
                            >
                                {isSubmitting ? <Loader2 className="animate-spin mr-2" size={20} /> : "Змінити пароль"}
                            </button>
                        </Form>
                    )}
                </Formik>
            </div>
        </div>
    );
};

export default ResetPasswordPage;