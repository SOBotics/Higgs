export interface ProfileResultCommon {
    DurationMilliseconds: number;
    HasCustomTimings: boolean;
    HasDuplicateCustomTimings: boolean;
    Id: string;
    Name: string;
    CustomTimingStats?: any;
}

export interface ProfileResultChild extends ProfileResultCommon {
    Depth: number;
    DurationWithoutChildrenMilliseconds: number;
    IsTrivial: boolean;
    ParentTimingId: string;
    StartMilliseconds: number;
    Children: ProfileResultChild[];
}

export interface ProfileResult extends ProfileResultCommon {
    ClientTimings: null;
    CustomLinks: any;
    HasTrivialTimings: boolean;
    MachineName: string;
    Root: ProfileResultChild;
    Started: Date;
    Storage: null;
    TrivialMilliseconds: number;
    User: string;
}
