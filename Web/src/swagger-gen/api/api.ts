export * from './admin.service';
import { AdminService } from './admin.service';
export * from './bot.service';
import { BotService } from './bot.service';
export * from './reviewer.service';
import { ReviewerService } from './reviewer.service';
export const APIS = [AdminService, BotService, ReviewerService];
