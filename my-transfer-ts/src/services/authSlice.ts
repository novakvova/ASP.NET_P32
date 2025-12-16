import {createSlice, type PayloadAction} from "@reduxjs/toolkit";
import type {UserTokenInfo} from "../Interfaces/User/UserTokenInfo.ts";
import {jwtDecode} from "jwt-decode";

const getUserFromToken = (token: string) : UserTokenInfo | null  => {
    try {
        if(!token)
            return null;
        const decode = jwtDecode<UserTokenInfo>(token);
        return decode ?? null;
    }
    catch (err) {
        console.error("Invalid token", err);
        return null;
    }
}

const token = localStorage.token;
const user = getUserFromToken(token);

const initialState = {
    user: user
}

const authSlice = createSlice({
    name: 'auth',
    initialState: initialState,
    reducers: {
        loginSuccess: (state, action: PayloadAction<string>) => {
            const authToken = action.payload;
            const user = getUserFromToken(authToken);
            if (user) {
                state.user = user;
                localStorage.token = authToken;
            }
        },
        logout: (state) => {
            state.user = null;
            localStorage.removeItem('token');
        },
    }
});

export const { loginSuccess, logout } = authSlice.actions;

export default authSlice.reducer;
