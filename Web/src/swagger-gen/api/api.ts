export * from './admin.service';
import { AdminService } from './admin.service';
export * from './bot.service';
import { BotService } from './bot.service';
export * from './file.service';
import { FileService } from './file.service';
export * from './reviewer.service';
import { ReviewerService } from './reviewer.service';
export const APIS = [AdminService, BotService, FileService, ReviewerService];
