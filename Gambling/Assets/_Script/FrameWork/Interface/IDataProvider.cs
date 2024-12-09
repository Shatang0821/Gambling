namespace FrameWork.Component
{
    public interface IDataProvider
    { 
        float GetData(string key); // 
        bool HasData(string key); // 
        void AddData(string key, float value); // 
        void RemoveData(string key); // 
    }
}