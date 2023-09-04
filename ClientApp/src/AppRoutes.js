import ApiAuthorzationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import { Reserve } from './pages/Reserve';
import { Home } from "./pages/Home";
import { Rooms } from "./pages/Rooms";

const AppRoutes = [
    {
        index: true,
        element: <Home />
    },
    {
        path: '/rooms',
        element: <Rooms />
    },
    {
        path: '/reserve',
        element: <Reserve />
    },
    ...ApiAuthorzationRoutes
];

export default AppRoutes;
