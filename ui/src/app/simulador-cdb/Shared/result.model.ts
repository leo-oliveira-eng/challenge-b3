import { Maybe } from './maybe.model';
import { Message } from "./message.model";

export class Result<T = {}>{
    failed!: boolean;
    messages!: Message[];
    data?: Maybe<T>;
}