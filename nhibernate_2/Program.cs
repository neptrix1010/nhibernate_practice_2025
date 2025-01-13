using NHibernate;
using nhibernate_2.Helpers;
using nhibernate_2.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

// Register NHibernate SessionFactory
builder.Services.AddSingleton<ISessionFactory>(provider =>
{
    return NHibernateHelper.CreateSessionFactory();
});

// Register ISession as scoped (one per request)
builder.Services.AddScoped<NHibernate.ISession>(provider =>
{
    var sessionFactory = provider.GetService<ISessionFactory>();
    return sessionFactory.OpenSession();
});

// Register repositories
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<CategoryRepository>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
