### dapper + extendtions 封装的一个仓储

主要功能包括 
+ 实现 DapperExtensions的IClassMapper接口实现自定义Column
+ 实现 自定义sql映射给DTO
+ 插入时返回ID
+ 加入非UUID非自增列的自定义主键映射

剩余实现
+ 注入 sql日志

