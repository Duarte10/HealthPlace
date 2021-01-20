import { Visit } from './Visit';

export type VisitorOverview = {
    id: string;
    name: string;
    email: string;
    mobile: string;
    visits: Visit[];
    notifications: [];
    positiveCases: [];
    activeTab: string;
}