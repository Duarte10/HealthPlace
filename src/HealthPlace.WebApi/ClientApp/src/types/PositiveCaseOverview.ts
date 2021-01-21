export type PositiveCaseOverview = {
    id: string;
    visitorId: string;
    visitorName: string;
    activeTab: string;
    allUsersNotified: boolean;
    visitDate: Date;
    collidingVisits: [];
}