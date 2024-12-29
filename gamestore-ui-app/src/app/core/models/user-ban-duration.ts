export interface UserBanDuration {
  interval: Interval;
  value: number;
  description: string;
}

export enum Interval {
  Hours = 0,
  Days = 1,
  Weeks = 2,
  Months = 3,
  Permanent = 4,
}
