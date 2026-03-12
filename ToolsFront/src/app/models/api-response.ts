export interface ApiResponse<T>{
    sucess: boolean;
    data: T;
    errors: string[] | null;
}