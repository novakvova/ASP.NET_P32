import { configureStore, combineReducers } from '@reduxjs/toolkit';
// import {countryApi} from "../services/CountryService/CountryService.ts";
// import {cityApi} from "../services/CityService/CityService.ts";
// import {googleApi} from "../services/GoogleAuthServise/GoogleAuthServise.ts";
import {authApi} from "../services/authApi.ts";
import {type TypedUseSelectorHook, useDispatch, useSelector} from "react-redux";
// import {authorizedUserApi} from "../services/AuthorizedUser/AuthorizedUserService.ts";
import authReducer from "../services/authSlice.ts";

const rootReducer = combineReducers({

    // [countryApi.reducerPath]: countryApi.reducer,
    // [cityApi.reducerPath]: cityApi.reducer,
    // [googleApi.reducerPath]: googleApi.reducer,
    [authApi.reducerPath]: authApi.reducer,
    auth: authReducer
    // [authorizedUserApi.reducerPath]: authorizedUserApi.reducer,
});

export const setupStore = () => {
    return configureStore({
        reducer: rootReducer,
        middleware: (getDefaultMiddleware) =>
            getDefaultMiddleware().concat(
                // countryApi.middleware,
                // cityApi.middleware,
                // googleApi.middleware,
                authApi.middleware,
                // authorizedUserApi.middleware
            ),

    });
};


export type RootState = ReturnType<typeof rootReducer>;
export type AppStore = ReturnType<typeof setupStore>;
export type AppDispatch = AppStore['dispatch'];
//dispatch - дає команди в redux - у редюсер, що треба, що виконати
export const useAppDispatch: () => AppDispatch = useDispatch
//selector - отримує дані з redux у будь-якому місці
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector